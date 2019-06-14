# BenchBlasMKL
Benchmark Blas matrix multiplication with monodevelop. CPU i7-7500U-2.90Ghz

Assuming libblas.so and libmkl_rt.so are avalaible from ldconfig.

### Output
```
DotNet Matrix Multiplication float (60 40) x (40 80) = (60 80) Time = 2 ms
DotNet Matrix Multiplication float (120 80) x (80 160) = (120 160) Time = 12 ms
DotNet Matrix Multiplication float (240 160) x (160 320) = (240 320) Time = 81 ms
DotNet Matrix Multiplication float (480 320) x (320 640) = (480 640) Time = 446 ms

Netlib BLAS SGEMM float (480 320) x (320 640) = (480 640) Time = 1 ms
Netlib BLAS SGEMM float (960 640) x (640 1280) = (960 1280) Time = 11 ms
Netlib BLAS SGEMM float (1920 1280) x (1280 2560) = (1920 2560) Time = 108 ms
Netlib BLAS SGEMM float (3840 2560) x (2560 5120) = (3840 5120) Time = 919 ms

Intel MKL BLAS SGEMM float (480 320) x (320 640) = (480 640) Time = 1 ms
Intel MKL BLAS SGEMM float (960 640) x (640 1280) = (960 1280) Time = 10 ms
Intel MKL BLAS SGEMM float (1920 1280) x (1280 2560) = (1920 2560) Time = 117 ms
Intel MKL BLAS SGEMM float (3840 2560) x (2560 5120) = (3840 5120) Time = 665 ms

DotNet Matrix Multiplication double (60 40) x (40 80) = (60 80) Time = 0 ms
DotNet Matrix Multiplication double (120 80) x (80 160) = (120 160) Time = 8 ms
DotNet Matrix Multiplication double (240 160) x (160 320) = (240 320) Time = 56 ms
DotNet Matrix Multiplication double (480 320) x (320 640) = (480 640) Time = 541 ms

Netlib BLAS DGEMM double (480 320) x (320 640) = (480 640) Time = 3 ms
Netlib BLAS DGEMM double (960 640) x (640 1280) = (960 1280) Time = 24 ms
Netlib BLAS DGEMM double (1920 1280) x (1280 2560) = (1920 2560) Time = 212 ms
Netlib BLAS DGEMM double (3840 2560) x (2560 5120) = (3840 5120) Time = 1553 ms

Intel MKL BLAS DGEMM double (480 320) x (320 640) = (480 640) Time = 3 ms
Intel MKL BLAS DGEMM double (960 640) x (640 1280) = (960 1280) Time = 28 ms
Intel MKL BLAS DGEMM double (1920 1280) x (1280 2560) = (1920 2560) Time = 158 ms
Intel MKL BLAS DGEMM double (3840 2560) x (2560 5120) = (3840 5120) Time = 1292 ms


```