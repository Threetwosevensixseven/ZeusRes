# ZeusRes
A simple resolution optimizer for [Simon Brattel](http://www.desdes.com/)'s [Zeus-ish](http://www.desdes.com/products/oldfiles/zeus.htm) Z80 cross assembler.

At lower resolutions like 1366x766, a few important buttons get cropped off the bottom of the emulator panel. This simple app hides some of less-used controls and moves the buttons into their place:

| **Before**    |   **After**   |
|:-------------:|:-------------:|
| ![Before](https://raw.githubusercontent.com/Threetwosevensixseven/ZeusRes/master/Images/before.png) | ![After](https://raw.githubusercontent.com/Threetwosevensixseven/ZeusRes/master/Images/after.png) |

By changing your app shortcut from *Zeus-ish* to to this helper app, the reolution optimization becomes pretty seamless.

## Prerequisites
1. Install [.NET Framework 4.6.1](https://www.microsoft.com/en-us/download/details.aspx?id=49981).
2. Install [Zeus-ish](http://www.desdes.com/products/oldfiles/zeus.htm).

## Setup
1. Exit *Zeus-ish* if it was previously running.
2. Copy `ZeusRes\bin\Debug\ZeusRes.exe` to the directory *Zeus-ish* is located in.
3. Change your *Zeus-ish* Windows shortcut to point to `ZeusRes.exe` instead.
4. Double-click the amended shortcut.
5. If you are prompted "Where is Zeus-ish located?" browse to `ZeusRes.exe` and click *OK*.
6. *Zeus-ish* will be launched, and the emulator panel will be reorganized to fit your lower resolution.

## Instructions
Every time you click the shortcut, it will open *Zeus-ish* if it not already open. Then the emulator panel will be reorganized to fit your lower resolution.

## Licence
ZeusRes is released under the [MIT Licence](https://github.com/Threetwosevensixseven/ZeusRes/blob/master/LICENSE).
