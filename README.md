# BenchBlasMKL
Benchmark Blas matrix multiplication with monodevelop. CPU i7-7500U-2.90Ghz

Assuming libblas.so and libmkl_rt.so are avalaible from ldconfig.

### Output
```
DotNetMatrixMultiplication
MatrixDouble A [2x2]
    1.00    2.00
    3.00    4.00
MatrixDouble B [2x2]
   -1.00   -2.00
   -3.00   -4.00
MatrixDouble C = A x B [2x2]
   -7.00  -10.00
  -15.00  -22.00

BlasMatrixMultiplication
MatrixDouble A [2x2]
    1.00    2.00
    3.00    4.00
MatrixDouble B [2x2]
   -1.00   -2.00
   -3.00   -4.00
MatrixDouble C = A x B [2x2]
   -7.00  -10.00
  -15.00  -22.00

MklMatrixMultiplication
MatrixDouble A [2x2]
    1.00    2.00
    3.00    4.00
MatrixDouble B [2x2]
   -1.00   -2.00
   -3.00   -4.00
MatrixDouble C = A x B [2x2]
   -7.00  -10.00
  -15.00  -22.00

DotNetMatrixMultiplication
MatrixFloat A [2x2]
    1.00    2.00
    3.00    4.00
MatrixFloat B [2x2]
   -1.00   -2.00
   -3.00   -4.00
MatrixFloat C = A x B [2x2]
   -7.00  -10.00
  -15.00  -22.00

BlasMatrixMultiplication
MatrixFloat A [2x2]
    1.00    2.00
    3.00    4.00
MatrixFloat B [2x2]
   -1.00   -2.00
   -3.00   -4.00
MatrixFloat C = A x B [2x2]
   -7.00  -10.00
  -15.00  -22.00

MklMatrixMultiplication
MatrixFloat A [2x2]
    1.00    2.00
    3.00    4.00
MatrixFloat B [2x2]
   -1.00   -2.00
   -3.00   -4.00
MatrixFloat C = A x B [2x2]
   -7.00  -10.00
  -15.00  -22.00


DotNetMatrixMultiplication float (60 40) x (40 80) = (60 80) Time = 1 ms
DotNetMatrixMultiplication float (120 80) x (80 160) = (120 160) Time = 10 ms
DotNetMatrixMultiplication float (240 160) x (160 320) = (240 320) Time = 83 ms
DotNetMatrixMultiplication float (480 320) x (320 640) = (480 640) Time = 486 ms

BlasMatrixMultiplication   float (480 320) x (320 640) = (480 640) Time = 8 ms
BlasMatrixMultiplication   float (960 640) x (640 1280) = (960 1280) Time = 12 ms
BlasMatrixMultiplication   float (1920 1280) x (1280 2560) = (1920 2560) Time = 107 ms
BlasMatrixMultiplication   float (3840 2560) x (2560 5120) = (3840 5120) Time = 859 ms

MklMatrixMultiplication    float (480 320) x (320 640) = (480 640) Time = 8 ms
MklMatrixMultiplication    float (960 640) x (640 1280) = (960 1280) Time = 10 ms
MklMatrixMultiplication    float (1920 1280) x (1280 2560) = (1920 2560) Time = 116 ms
MklMatrixMultiplication    float (3840 2560) x (2560 5120) = (3840 5120) Time = 626 ms

DotNetMatrixMultiplication double (60 40) x (40 80) = (60 80) Time = 0 ms
DotNetMatrixMultiplication double (120 80) x (80 160) = (120 160) Time = 8 ms
DotNetMatrixMultiplication double (240 160) x (160 320) = (240 320) Time = 58 ms
DotNetMatrixMultiplication double (480 320) x (320 640) = (480 640) Time = 539 ms

BlasMatrixMultiplication   double (480 320) x (320 640) = (480 640) Time = 4 ms
BlasMatrixMultiplication   double (960 640) x (640 1280) = (960 1280) Time = 24 ms
BlasMatrixMultiplication   double (1920 1280) x (1280 2560) = (1920 2560) Time = 227 ms
BlasMatrixMultiplication   double (3840 2560) x (2560 5120) = (3840 5120) Time = 1582 ms

MklMatrixMultiplication    double (480 320) x (320 640) = (480 640) Time = 11 ms
MklMatrixMultiplication    double (960 640) x (640 1280) = (960 1280) Time = 20 ms
MklMatrixMultiplication    double (1920 1280) x (1280 2560) = (1920 2560) Time = 174 ms
MklMatrixMultiplication    double (3840 2560) x (2560 5120) = (3840 5120) Time = 1330 ms


```