using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace BenchBlasMKL
{
    class MainClass
    {
        [DllImport("blas", EntryPoint = "cblas_dgemm")]
        static extern void BLAS_dgemm(int Order, int TransA, int TransB,
            int M, int N, int K,
            double alpha, double[] A, int lda, double[] B, int ldb,
            double beta, double[] C, int ldc);

        [DllImport("mkl_rt", EntryPoint = "cblas_dgemm")]
        static extern void MKL_dgemm(int Order, int TransA, int TransB,
            int M, int N, int K,
            double alpha, double[] A, int lda, double[] B, int ldb,
            double beta, double[] C, int ldc);

        static void BlasMatrixMultiplication(int m, int n, int k, double[] A, double[] B, double[] C)
        {
            if (m * k != A.Length || n * k != B.Length || m * n != C.Length)
                throw new ArgumentException();

            BLAS_dgemm(101, 111, 111, m, n, k, 1.0, A, k, B, n, 0.0, C, n);
        }

        static void MklMatrixMultiplication(int m, int n, int k, double[] A, double[] B, double[] C)
        {
            if (m * k != A.Length || n * k != B.Length || m * n != C.Length)
                throw new ArgumentException();

            MKL_dgemm(101, 111, 111, m, n, k, 1.0, A, k, B, n, 0.0, C, n);
        }

        static void DotNetMatrixMultiplication(int m, int n, int k, double[] A, double[] B, double[] C)
        {
            if (m * k != A.Length || n * k != B.Length || m * n != C.Length)
                throw new ArgumentException();

            int i, j, k0, idxA, idxB, idxC;
            double sum;

            for (i = 0; i < m; ++i)
            {
                for (j = 0; j < n; ++j)
                {
                    idxC = i * n + j;
                    sum = 0;
                    for (k0 = 0; k0 < k; ++k0)
                    {
                        idxA = i * k + k0;
                        idxB = k0 * n + j;
                        sum += A[idxA] * B[idxB];
                    }
                    C[idxC] = sum;
                }
            }
        }

        static void DisplayMatrix(int m, int n, double[] M, string info = "M")
        {
            if (m * n != M.Length)
                throw new ArgumentException();

            Console.WriteLine($"Matrix {info} [{m}x{n}]");
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Console.Write($"{M[i * n + j],8:F2}");
                }
                Console.WriteLine();
            }
        }

        static void TestDotNet()
        {
            int m = 2, n = 2, k = 2;
            double[] A = Enumerable.Range(1, 4).Select(i => (double)i).ToArray();
            double[] B = Enumerable.Range(1, 4).Select(i => -(double)i).ToArray();
            double[] C = new double[4];

            Console.WriteLine("DotNetMatrixMultiplication");
            DotNetMatrixMultiplication(m, n, k, A, B, C);
            DisplayMatrix(m, k, A, "A");
            DisplayMatrix(k, n, B, "B");
            DisplayMatrix(m, n, C, "C = A x B");
            Console.WriteLine();
        }

        static void TestMKL()
        {
            int m = 2, n = 2, k = 2;
            double[] A = Enumerable.Range(1, 4).Select(i => (double)i).ToArray();
            double[] B = Enumerable.Range(1, 4).Select(i => -(double)i).ToArray();
            double[] C = new double[4];

            Console.WriteLine("MklMatrixMultiplication");
            MklMatrixMultiplication(m, n, k, A, B, C);
            DisplayMatrix(m, k, A, "A");
            DisplayMatrix(k, n, B, "B");
            DisplayMatrix(m, n, C, "C = A x B");
            Console.WriteLine();
        }

        static void TestBLAS()
        {
            int m = 2, n = 2, k = 2;
            double[] A = Enumerable.Range(1, 4).Select(i => (double)i).ToArray();
            double[] B = Enumerable.Range(1, 4).Select(i => -(double)i).ToArray();
            double[] C = new double[4];

            Console.WriteLine("BlasMatrixMultiplication");
            BlasMatrixMultiplication(m, n, k, A, B, C);
            DisplayMatrix(m, k, A, "A");
            DisplayMatrix(k, n, B, "B");
            DisplayMatrix(m, n, C, "C = A x B");
            Console.WriteLine();
        }

        static Random random = new Random();
        static void Bench1(int m, int n, int k, bool display = false)
        {
            if (m <= 0 || n <= 0 || k <= 0)
                throw new ArgumentException();

            double[] A = Enumerable.Range(0, m * k).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] B = Enumerable.Range(0, k * n).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] C0 = new double[m * n];
            double[] C1 = new double[m * n];
            double[] C2 = new double[m * n];

            var sw0 = Stopwatch.StartNew();
            DotNetMatrixMultiplication(m, n, k, A, B, C0);
            Console.WriteLine($"DotNetMatrixMultiplication ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");

            var sw1 = Stopwatch.StartNew();
            BlasMatrixMultiplication(m, n, k, A, B, C1);
            Console.WriteLine($"BlasMatrixMultiplication   ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw1.ElapsedMilliseconds} ms");

            var sw2 = Stopwatch.StartNew();
            MklMatrixMultiplication(m, n, k, A, B, C2);
            Console.WriteLine($"MklMatrixMultiplication    ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw2.ElapsedMilliseconds} ms");

            Console.WriteLine();

            if (display)
            {
                DisplayMatrix(m, n, C0, "Dotnet");
                DisplayMatrix(m, n, C1, "BLAS");
                DisplayMatrix(m, n, C2, "MKL");
                Console.WriteLine();
            }
        }

        static void BenchDotNet(int m, int n, int k)
        {
            if (m <= 0 || n <= 0 || k <= 0)
                throw new ArgumentException();

            double[] A = Enumerable.Range(0, m * k).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] B = Enumerable.Range(0, k * n).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] C = new double[m * n];

            var sw0 = Stopwatch.StartNew();
            DotNetMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"DotNetMatrixMultiplication ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        static void BenchBLAS(int m, int n, int k)
        {
            if (m <= 0 || n <= 0 || k <= 0)
                throw new ArgumentException();

            double[] A = Enumerable.Range(0, m * k).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] B = Enumerable.Range(0, k * n).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] C = new double[m * n];

            var sw0 = Stopwatch.StartNew();
            BlasMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"BlasMatrixMultiplication   ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        static void BenchMKL(int m, int n, int k)
        {
            if (m <= 0 || n <= 0 || k <= 0)
                throw new ArgumentException();

            double[] A = Enumerable.Range(0, m * k).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] B = Enumerable.Range(0, k * n).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] C = new double[m * n];

            var sw0 = Stopwatch.StartNew();
            MklMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"MklMatrixMultiplication    ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!\n");

            TestDotNet();
            TestMKL();
            TestBLAS();

            Bench1(300, 400, 200);
            Bench1(4, 5, 3, true);
            Console.WriteLine();

            int N = 4;
            int c = 2;
            for (int k = 0; k < N; ++k)
            {
                BenchDotNet(30 * c, 40 * c, 20 * c);
                c *= 2;
            }

            c = 16;
            for (int k = 0; k < N; ++k)
            {
                BenchBLAS(30 * c, 40 * c, 20 * c);
                c *= 2;
            }

            c = 16;
            for (int k = 0; k < N; ++k)
            {
                BenchMKL(30 * c, 40 * c, 20 * c);
                c *= 2;
            }
        }
    }
}
