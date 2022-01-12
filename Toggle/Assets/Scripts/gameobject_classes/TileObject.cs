using UnityEngine;
using UnityEngine.UI;

/* TileObject
 * Purpose:
 *      Attached to each Toggle on the gameboard.
 *      Responsible for allowing player to change the board state.
 */
[RequireComponent(typeof(Toggle))]
public class TileObject : MonoBehaviour
{
    #region fields
    private Gameboard gameboard;
    private Tile tile;
    private Toggle toggle; // TODO: change to button.
    [Header("Is Off Color Block")] // TODO: change to a ColorChange component
    public ColorBlock isOffColorBlock;
    [Header("Is On Color Block")]
    public ColorBlock isOnColorBlock;

    #endregion

    #region properties

    public Gameboard Gameboard { get => gameboard; }
    public Tile Tile { get => tile; }
    public Toggle Toggle { get => toggle; }

    #endregion

    #region monobehaviour

    void Start()
    {
        toggle = GetComponent<Toggle>();
        ChangeColor(false);
        toggle.onValueChanged.AddListener(OnToggleValueChange);
    }

    #endregion

    #region interface

    /* ConnectTile
     * Purpose:
     *      Acts as a setter for this gameobject since Monobehaviour does not handle constructors well.
     */
    public void ConnectTile(Gameboard gameboard, Tile tile)
    {
        if (gameboard != null && tile != null)
        {
            this.gameboard = gameboard;
            this.tile = tile;
        }
        else
            throw new System.Exception("Could not connect TileObject to gameboard and/or tile");
    }

    public void PerformToggle()
    {
        gameboard?.ToggleTile(tile);
        ChangeColor(tile.IsOn);
    }

    #endregion
    //TODO: change to onclick
    private void OnToggleValueChange(bool isOn)
    {
        ToggleCommand toggleCommand = new ToggleCommand(this);
        CommandManager.Instance.AddCommand(toggleCommand);
        toggleCommand.Execute();
    }

    private void ChangeColor(bool isOn)
    {
        if (isOn)
        {
            toggle.colors = isOnColorBlock;
        }
        else
        {
            toggle.colors = isOffColorBlock;
        }
    }
}
