# OpenMinesweeper
This is an open source minesweeper game developed as a simple use case for WPF and MVVM.

Currently, this application is divided in the following projects:
* OpenMinesweeper.Core

  Contains some core game logic, such as creating the cells/mines for the player. This was created using .NET Standard 2.0.

* OpenMinesweeper.NET

  Produces a WPF GUI application written with .NET Core 3.1. This makes use of the excellent [MVVMLight](https://github.com/lbugnion/mvvmlight), a cross-platform MVVM toolkit.

# Build requirements
* Visual Studio 2019 Community or higher
* .NET Core 3.1+

# How to play
1. Select "Game" in the menu strip, then "New".
2. Click the cells inside the window. Once a cell is clicked, it will reveal the number of adjacent cells that contain mines.
3. Avoid all mines to win the game.

# License
Licensed under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).
