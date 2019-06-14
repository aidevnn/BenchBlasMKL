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

        static void DisplayMatrix<T>(int m, int n, T[] M, string info = "M")
        {
            if (m * n != M.Length)
                throw new ArgumentException();

            Console.WriteLine($"Matrix{typeof(T).Name} {info} ({m} {n})");
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

        static void TestFloat(int m = 2, int n = 2, int k = 2)
        {
            float[] A = Enumerable.Range(1, m * k).Select(i => (float)random.Next(-9, 10)).ToArray();
            float[] B = Enumerable.Range(1, k * n).Select(i => -(float)random.Next(-9, 10)).ToArray();
            float[] C0 = new float[m * n];
            float[] C1 = new float[m * n];
            float[] C2 = new float[m * n];

            DotNetMatrixMultiplication(m, n, k, A, B, C0);
            MklMatrixMultiplication(m, n, k, A, B, C1);
            BlasMatrixMultiplication(m, n, k, A, B, C2);

            Console.WriteLine("Matrix Multiplication Float");
            DisplayMatrix(m, k, A, "A");
            DisplayMatrix(k, n, B, "B");
            DisplayMatrix(m, n, C0, ".NET C = A x B");
            DisplayMatrix(m, n, C1, "MKL BLAS  C = A x B");
            DisplayMatrix(m, n, C2, "Netlib BLAS C = A x B");

            Console.WriteLine();
        }

        static void TestDouble(int m = 2, int n = 2, int k = 2)
        {
            double[] A = Enumerable.Range(1, m * k).Select(i => (double)random.Next(-9, 10)).ToArray();
            double[] B = Enumerable.Range(1, k * n).Select(i => -(double)random.Next(-9, 10)).ToArray();
            double[] C0 = new double[m * n];
            double[] C1 = new double[m * n];
            double[] C2 = new double[m * n];

            DotNetMatrixMultiplication(m, n, k, A, B, C0);
            MklMatrixMultiplication(m, n, k, A, B, C1);
            BlasMatrixMultiplication(m, n, k, A, B, C2);

            Console.WriteLine("Matrix Multiplication Double");
            DisplayMatrix(m, k, A, "A");
            DisplayMatrix(k, n, B, "B");
            DisplayMatrix(m, n, C0, ".NET C = A x B");
            DisplayMatrix(m, n, C1, "MKL BLAS  C = A x B");
            DisplayMatrix(m, n, C2, "Netlib BLAS C = A x B");

            Console.WriteLine();
        }

        static (T[],T[],T[]) TupleMatrix<T>(int m, int n, int k)
        {
            if (m <= 0 || n <= 0 || k <= 0)
                throw new ArgumentException();

            T func(int i) => (T)Convert.ChangeType((random.NextDouble() * 2) - 1, typeof(T));
            T[] A = Enumerable.Range(0, m * k).Select(func).ToArray();
            T[] B = Enumerable.Range(0, k * n).Select(func).ToArray();
            T[] C = new T[m * n];

            return (A, B, C);
        }

        static void BenchDotNetfloat(int m, int n, int k)
        {
            (var A, var B, var C) = TupleMatrix<float>(m, n, k);

            var sw0 = Stopwatch.StartNew();
            DotNetMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"DotNet Matrix Multiplication float ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        static void BenchDotNetdouble(int m, int n, int k)
        {
            (var A, var B, var C) = TupleMatrix<double>(m, n, k);

            var sw0 = Stopwatch.StartNew();
            DotNetMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"DotNet Matrix Multiplication double ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        static void BenchBLASfloat(int m, int n, int k)
        {
            (var A, var B, var C) = TupleMatrix<float>(m, n, k);

            var sw0 = Stopwatch.StartNew();
            BlasMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"Netlib BLAS SGEMM float ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        static void BenchBLASdouble(int m, int n, int k)
        {
            (var A, var B, var C) = TupleMatrix<double>(m, n, k);

            var sw0 = Stopwatch.StartNew();
            BlasMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"Netlib BLAS DGEMM double ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        static void BenchMKLfloat(int m, int n, int k)
        {
            (var A, var B, var C) = TupleMatrix<float>(m, n, k);

            var sw0 = Stopwatch.StartNew();
            MklMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"Intel MKL BLAS SGEMM float ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        static void BenchMKLdouble(int m, int n, int k)
        {
            (var A, var B, var C) = TupleMatrix<double>(m, n, k);

            var sw0 = Stopwatch.StartNew();
            MklMatrixMultiplication(m, n, k, A, B, C);
            Console.WriteLine($"Intel MKL BLAS DGEMM double ({m} {k}) x ({k} {n}) = ({m} {n}) Time = {sw0.ElapsedMilliseconds} ms");
        }

        public static void Main(string[] args)
        {
            TestFloat();
            TestDouble(3,2,4);

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

            //long d = 3840 * 5120;
            //var lt = Enumerable.Range(0, (int)d).Select(i => (long)i).ToArray(); // Check big size array
            //var sum0 = d * (d - 1) / 2;
            //var sum1 = lt.Sum();
            //Console.WriteLine($"Array Int Sum. Length:{d} Formula:{sum0} Sum:{sum1} Diff:{sum0 - sum1}");
        }
    }
}
