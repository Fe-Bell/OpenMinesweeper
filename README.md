# OpenMinesweeper
This is an open source minesweeper game developed as a simple use case for [WPF](https://en.wikipedia.org/wiki/Windows_Presentation_Foundation) and [MVVM](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel).

Currently, this application is divided in the following projects:
* OpenMinesweeper.Core

  Contains some core game logic, such as creating the cells/mines for the player. This was created using .NET Standard 2.0. This project is cross-platform.
  
  For data persistency, the core uses [ReflectXMLDB](https://github.com/Fe-Bell/ReflectXMLDB), an XML-based database library written by the same author of this project.

* OpenMinesweeper.NET

  Produces a WPF GUI application written with .NET Core 3.1. This makes use of the excellent [MVVMLight](https://github.com/lbugnion/mvvmlight), a cross-platform MVVM toolkit. This project is Windows-only.

![sample1](Art/Samples/sample1.png=300x300)

# Build requirements
* [Visual Studio 2019 Community](https://visualstudio.microsoft.com/) or higher
* .NET Core 3.1+ or higher

# For players only
If you are only looking to play the game, make sure to have the runtime of .NET Core 3.1 (or higher) installed on your machine.

# How to play
1. Select "Game" in the menu strip, then "New". Select the size of the game grid and click "Play".
2. Click the cells inside the window. Once a cell is clicked, it will reveal the number of adjacent cells that contain mines.
3. Avoid all mines to win the game.

# License
Licensed under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).
