# Gameboard Data Structure
## Tile
The main game piece.

Core functionality:
- Changes state to true or false.
## Gameboard
The API to interact with the data structure of the game.

Generates a collection of Tiles and manipulates them.

Generates a collection of Hints that can be viewed.

Handles core game mechanics:
- Generates hints/solution.
- Checks Tile state and compares it with solution.
- Manipulates the state of the Tile collection.
- Changes the board size/difficulty.
## Hint
Contains the solution to the gameboard.

A collection of integers that represent the number of consecutive Tiles with their state set to true.

Core functionality:
- Stores solution.
