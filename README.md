# BenchBlasMKL
Benchmark Blas matrix multiplication

Assuming libblas.so and libmkl_rt.so are avalaible from ldconfig.

### Output
```
DotNetMatrixMultiplication
Matrix A [2x2]
    1.00    2.00
    3.00    4.00
Matrix B [2x2]
   -1.00   -2.00
   -3.00   -4.00
Matrix C = A x B [2x2]
   -7.00  -10.00
  -15.00  -22.00

MklMatrixMultiplication
Matrix A [2x2]
    1.00    2.00
    3.00    4.00
Matrix B [2x2]
   -1.00   -2.00
   -3.00   -4.00
Matrix C = A x B [2x2]
   -7.00  -10.00
  -15.00  -22.00

BlasMatrixMultiplication
Matrix A [2x2]
    1.00    2.00
    3.00    4.00
Matrix B [2x2]
   -1.00   -2.00
   -3.00   -4.00
Matrix C = A x B [2x2]
   -7.00  -10.00
  -15.00  -22.00

DotNetMatrixMultiplication (60 40) x (40 80) = (60 80) Time = 3 ms
DotNetMatrixMultiplication (120 80) x (80 160) = (120 160) Time = 17 ms
DotNetMatrixMultiplication (240 160) x (160 320) = (240 320) Time = 83 ms
DotNetMatrixMultiplication (480 320) x (320 640) = (480 640) Time = 548 ms

BlasMatrixMultiplication   (480 320) x (320 640) = (480 640) Time = 5 ms
BlasMatrixMultiplication   (960 640) x (640 1280) = (960 1280) Time = 22 ms
BlasMatrixMultiplication   (1920 1280) x (1280 2560) = (1920 2560) Time = 181 ms
BlasMatrixMultiplication   (3840 2560) x (2560 5120) = (3840 5120) Time = 1512 ms

MklMatrixMultiplication    (480 320) x (320 640) = (480 640) Time = 7 ms
MklMatrixMultiplication    (960 640) x (640 1280) = (960 1280) Time = 28 ms
MklMatrixMultiplication    (1920 1280) x (1280 2560) = (1920 2560) Time = 152 ms
MklMatrixMultiplication    (3840 2560) x (2560 5120) = (3840 5120) Time = 1223 ms

```