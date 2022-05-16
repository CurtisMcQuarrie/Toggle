# Layers
1. Model
2. Model-View
3. View

# Model
This is the gameboard_logic package.

It controls the data structure and underlying mechanisms to the game.

Contains:
- Tile.csharp
- Gameboard.csharp
- Hint.csharp

# Model-View
This is the gameboard_object package.

It attaches the model classes to Gameobjects inside Unity.

It calls appropriate **View** methods to generate the GUI and then attaches the **Model** to them.

It also connects the **Model** methods so that the user can interact with the GUI to manipulate the state of the game.

Contains:
- TileObject.csharp
- GameboardController.cshap

# View
This is the gameboard_gui package.

It controls the generation of GUI components.
