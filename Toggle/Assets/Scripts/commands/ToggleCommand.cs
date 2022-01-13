/* ToggleCommand
 * Purpose:
 *      Manipulates TileObjects by calling their PerformToggle method.
 *      Used in Command Pattern within CommandManager class.
 */
public class ToggleCommand : ICommand
{
    #region fields

    private TileObject tile;

    #endregion

    #region constructors

    public ToggleCommand(TileObject tileObject)
    {
        tile = tileObject;
    }

    #endregion

    #region interface

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

    #endregion

    #region utility

    private void PerformToggle()
    {
        tile?.PerformToggle();
    }

    #endregion
}
