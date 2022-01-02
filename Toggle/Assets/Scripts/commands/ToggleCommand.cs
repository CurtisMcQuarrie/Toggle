using UnityEngine;

public class ToggleCommand : ICommand
{
    private Gameboard gameboard;
    private Tile tile;

    public ToggleCommand(Gameboard gameboard, Tile tile)
    {
        this.gameboard = gameboard;
        this.tile = tile;
    }

    public void Execute()
    {
        PerformToggle();
        Debug.Log("Enabled state set to " + tile.Enabled);
    }

    public void Redo()
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        PerformToggle();
    }

    private void PerformToggle()
    {
        if (gameboard != null)
        {
            gameboard.Toggle(tile);
        }
    }
}
