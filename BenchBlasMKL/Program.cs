using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace BenchBlasMKL
{
    class MainClass
    {
        [DllImport("blas", EntryPoint = "cblas_sgemm")]
        static extern void BLAS_sgemm(int Order, int TransA, int TransB,
            int M, int N, int K,
            float alpha, float[] A, int lda, float[] B, int ldb,
            float beta, float[] C, int ldc);

        [DllImport("blas", EntryPoint = "cblas_dgemm")]
        static extern void BLAS_dgemm(int Order, int TransA, int TransB,
            int M, int N, int K,
            double alpha, double[] A, int lda, double[] B, int ldb,
            double beta, double[] C, int ldc);

        [DllImport("mkl_rt", EntryPoint = "cblas_sgemm")]
        static extern void MKL_sgemm(int Order, int TransA, int TransB,
            int M, int N, int K,
            float alpha, float[] A, int lda, float[] B, int ldb,
            float beta, float[] C, int ldc);

        [DllImport("mkl_rt", EntryPoint = "cblas_dgemm")]
        static extern void MKL_dgemm(int Order, int TransA, int TransB,
            int M, int N, int K,
            double alpha, double[] A, int lda, double[] B, int ldb,
            double beta, double[] C, int ldc);

        static void BlasMatrixMultiplication(int m, int n, int k, float[] A, float[] B, float[] C)
        {
            if (m * k != A.Length || n * k != B.Length || m * n != C.Length)
                throw new ArgumentException();

            BLAS_sgemm(101, 111, 111, m, n, k, 1.0f, A, k, B, n, 0.0f, C, n);
        }

        static void BlasMatrixMultiplication(int m, int n, int k, double[] A, double[] B, double[] C)
        {
            if (m * k != A.Length || n * k != B.Length || m * n != C.Length)
                throw new ArgumentException();

            BLAS_dgemm(101, 111, 111, m, n, k, 1.0, A, k, B, n, 0.0, C, n);
        }

        static void MklMatrixMultiplication(int m, int n, int k, float[] A, float[] B, float[] C)
        {
            if (m * k != A.Length || n * k != B.Length || m * n != C.Length)
                throw new ArgumentException();

            MKL_sgemm(101, 111, 111, m, n, k, 1.0f, A, k, B, n, 0.0f, C, n);
        }

        static void MklMatrixMultiplication(int m, int n, int k, double[] A, double[] B, double[] C)
        {
            if (m * k != A.Length || n * k != B.Length || m * n != C.Length)
                throw new ArgumentException();

            MKL_dgemm(101, 111, 111, m, n, k, 1.0, A, k, B, n, 0.0, C, n);
        }

        static void DotNetMatrixMultiplication(int m, int n, int k, float[] A, float[] B, float[] C)
        {
            if (m * k != A.Length || n * k != B.Length || m * n != C.Length)
                throw new ArgumentException();

            int i, j, k0, idxA, idxB, idxC;
            float sum;

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

        static void DisplayMatrix(int m, int n, float[] M, string info = "M")
        {
            if (m * n != M.Length)
                throw new ArgumentException();

            Console.WriteLine($"MatrixFloat {info} [{m}x{n}]");
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Console.Write($"{M[i * n + j],8:F2}");
                }
                Console.WriteLine();
            }
        }

        static void DisplayMatrix(int m, int n, double[] M, string info = "M")
        {
            if (m * n != M.Length)
                throw new ArgumentException();

            Console.WriteLine($"MatrixDouble {info} [{m}x{n}]");
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Console.Write($"{M[i * n + j],8:F2}");
                }
                Console.WriteLine();
            }
        }

        static Random random = new Random();
        static void TestDotNetfloat()
        {
            int m = 2, n = 2, k = 2;
            float[] A = Enumerable.Range(1, 4).Select(i => (float)i).ToArray();
            float[] B = Enumerable.Range(1, 4).Select(i => -(float)i).ToArray();
            float[] C = new float[4];

            Console.WriteLine("DotNetMatrixMultiplication");
            DotNetMatrixMultiplication(m, n, k, A, B, C);
            DisplayMatrix(m, k, A, "A");
            DisplayMatrix(k, n, B, "B");
            DisplayMatrix(m, n, C, "C = A x B");
            Console.WriteLine();
        }

        static void TestDotNetdouble()
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

        static void TestMKLfloat()
        {
            int m = 2, n = 2, k = 2;
            float[] A = Enumerable.Range(1, 4).Select(i => (float)i).ToArray();
            float[] B = Enumerable.Range(1, 4).Select(i => -(float)i).ToArray();
            float[] C = new float[4];

            Console.WriteLine("MklMatrixMultiplication");
            MklMatrixMultiplication(m, n, k, A, B, C);
            DisplayMatrix(m, k, A, "A");
            DisplayMatrix(k, n, B, "B");
            DisplayMatrix(m, n, C, "C = A x B");
            Console.WriteLine();
        }

        static void TestMKLdouble()
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

        static void TestBLASfloat()
        {
            int m = 2, n = 2, k = 2;
            float[] A = Enumerable.Range(1, 4).Select(i => (float)i).ToArray();
            float[] B = Enumerable.Range(1, 4).Select(i => -(float)i).ToArray();
            float[] C = new float[4];

            Console.WriteLine("BlasMatrixMultiplication");
            BlasMatrixMultiplication(m, n, k, A, B, C);
            DisplayMatrix(m, k, A, "A");
            DisplayMatrix(k, n, B, "B");
            DisplayMatrix(m, n, C, "C = A x B");
            Console.WriteLine();
        }

        static void TestBLASdouble()
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

        static void BenchDotNetfloat(int m, int n, int k)
        {
            if (m <= 0 || n <= 0 || k <= 0)
                throw new ArgumentException();

            float[] A = Enumerable.Range(0, m * k).Select(i => (float)random.NextDouble() * 2 - 1).ToArray();
            float[] B = Enumerable.Range(0, k * n).Select(i => (float)random.NextDouble() * 2 - 1).ToArray();
            float[] C = new float[m * n];

            var sw0 = Stopwatch.StartNew();
            DotNetMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"DotNetMatrixMultiplication float ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        static void BenchDotNetdouble(int m, int n, int k)
        {
            if (m <= 0 || n <= 0 || k <= 0)
                throw new ArgumentException();

            double[] A = Enumerable.Range(0, m * k).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] B = Enumerable.Range(0, k * n).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] C = new double[m * n];

            var sw0 = Stopwatch.StartNew();
            DotNetMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"DotNetMatrixMultiplication double ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        static void BenchBLASfloat(int m, int n, int k)
        {
            if (m <= 0 || n <= 0 || k <= 0)
                throw new ArgumentException();

            float[] A = Enumerable.Range(0, m * k).Select(i => (float)random.NextDouble() * 2 - 1).ToArray();
            float[] B = Enumerable.Range(0, k * n).Select(i => (float)random.NextDouble() * 2 - 1).ToArray();
            float[] C = new float[m * n];

            var sw0 = Stopwatch.StartNew();
            BlasMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"BlasMatrixMultiplication   float ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        static void BenchBLASdouble(int m, int n, int k)
        {
            if (m <= 0 || n <= 0 || k <= 0)
                throw new ArgumentException();

            double[] A = Enumerable.Range(0, m * k).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] B = Enumerable.Range(0, k * n).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] C = new double[m * n];

            var sw0 = Stopwatch.StartNew();
            BlasMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"BlasMatrixMultiplication   double ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        static void BenchMKLfloat(int m, int n, int k)
        {
            if (m <= 0 || n <= 0 || k <= 0)
                throw new ArgumentException();

            float[] A = Enumerable.Range(0, m * k).Select(i => (float)random.NextDouble() * 2 - 1).ToArray();
            float[] B = Enumerable.Range(0, k * n).Select(i => (float)random.NextDouble() * 2 - 1).ToArray();
            float[] C = new float[m * n];

            var sw0 = Stopwatch.StartNew();
            MklMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"MklMatrixMultiplication    float ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        static void BenchMKLdouble(int m, int n, int k)
        {
            if (m <= 0 || n <= 0 || k <= 0)
                throw new ArgumentException();

            double[] A = Enumerable.Range(0, m * k).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] B = Enumerable.Range(0, k * n).Select(i => random.NextDouble() * 2 - 1).ToArray();
            double[] C = new double[m * n];

            var sw0 = Stopwatch.StartNew();
            MklMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"MklMatrixMultiplication    double ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        public static void Main(string[] args)
        {
            TestDotNetdouble();
            TestBLASdouble();
            TestMKLdouble();

            TestDotNetfloat();
            TestBLASfloat();
            TestMKLfloat();

            int N = 4;
            int c = 2;
            for (int k = 0; k < N; ++k, c *= 2)
                BenchDotNetfloat(30 * c, 40 * c, 20 * c);
            Console.WriteLine();

            c = 16;
            for (int k = 0; k < N; ++k, c *= 2)
                BenchBLASfloat(30 * c, 40 * c, 20 * c);
            Console.WriteLine();

            c = 16;
            for (int k = 0; k < N; ++k, c *= 2)
                BenchMKLfloat(30 * c, 40 * c, 20 * c);
            Console.WriteLine();

            c = 2;
            for (int k = 0; k < N; ++k, c *= 2)
                BenchDotNetdouble(30 * c, 40 * c, 20 * c);
            Console.WriteLine();

            c = 16;
            for (int k = 0; k < N; ++k, c *= 2)
                BenchBLASdouble(30 * c, 40 * c, 20 * c);
            Console.WriteLine();

            c = 16;
            for (int k = 0; k < N; ++k, c *= 2)
                BenchMKLdouble(30 * c, 40 * c, 20 * c);
            Console.WriteLine();

        }
    }
}
