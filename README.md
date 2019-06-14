# BenchBlasMKL
Benchmark Blas matrix multiplicatin

Assuming libblas.so and libmkl_rt.so are in avalaible from ldconfig.

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

DotNetMatrixMultiplication (300 200) x (200 400) = (300 400) Time = 147 ms
BlasMatrixMultiplication   (300 200) x (200 400) = (300 400) Time = 2 ms
MklMatrixMultiplication    (300 200) x (200 400) = (300 400) Time = 7 ms

DotNetMatrixMultiplication (4 3) x (3 5) = (4 5) Time = 0 ms
BlasMatrixMultiplication   (4 3) x (3 5) = (4 5) Time = 0 ms
MklMatrixMultiplication    (4 3) x (3 5) = (4 5) Time = 0 ms

Matrix Dotnet [4x5]
   -0.21   -0.01    0.41   -0.84   -0.81
    0.28    0.12   -0.94   -0.36    0.14
    0.13    0.10   -0.56    0.19    0.38
    0.04    0.36   -1.20    0.07    0.54
Matrix BLAS [4x5]
   -0.21   -0.01    0.41   -0.84   -0.81
    0.28    0.12   -0.94   -0.36    0.14
    0.13    0.10   -0.56    0.19    0.38
    0.04    0.36   -1.20    0.07    0.54
Matrix MKL [4x5]
   -0.21   -0.01    0.41   -0.84   -0.81
    0.28    0.12   -0.94   -0.36    0.14
    0.13    0.10   -0.56    0.19    0.38
    0.04    0.36   -1.20    0.07    0.54


DotNetMatrixMultiplication (60 40) x (40 80) = (60 80) Time = 1 ms
DotNetMatrixMultiplication (120 80) x (80 160) = (120 160) Time = 16 ms
DotNetMatrixMultiplication (240 160) x (160 320) = (240 320) Time = 81 ms
DotNetMatrixMultiplication (480 320) x (320 640) = (480 640) Time = 550 ms
BlasMatrixMultiplication   (480 320) x (320 640) = (480 640) Time = 3 ms
BlasMatrixMultiplication   (960 640) x (640 1280) = (960 1280) Time = 25 ms
BlasMatrixMultiplication   (1920 1280) x (1280 2560) = (1920 2560) Time = 166 ms
BlasMatrixMultiplication   (3840 2560) x (2560 5120) = (3840 5120) Time = 1579 ms
MklMatrixMultiplication    (480 320) x (320 640) = (480 640) Time = 5 ms
MklMatrixMultiplication    (960 640) x (640 1280) = (960 1280) Time = 21 ms
MklMatrixMultiplication    (1920 1280) x (1280 2560) = (1920 2560) Time = 152 ms
MklMatrixMultiplication    (3840 2560) x (2560 5120) = (3840 5120) Time = 1224 ms
```