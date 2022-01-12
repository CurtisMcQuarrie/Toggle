using UnityEngine;

public class ToggleCommand : ICommand
{
    private TileObject tile;

    public ToggleCommand(TileObject tileObject)
    {
        tile = tileObject;
    }

    public void Execute()
    {
        PerformToggle();
    }

    public void Redo()
    {
        PerformToggle();
    }

    public void Undo()
    {
        PerformToggle();
    }

    private void PerformToggle()
    {
        tile?.PerformToggle();
    }
}
