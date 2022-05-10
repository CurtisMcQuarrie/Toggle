using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* TileObject
 * Purpose:
 *      Attached to each Button on the gameboard.
 *      Responsible for allowing player to change the board state.
 */
[RequireComponent(typeof(Button))]
public class TileObject : MonoBehaviour
{
    #region fields

    private Gameboard gameboard;
    private Tile tile;
    private Button button;
    private List<ITileObjectSubscriber> subscribers;

    #endregion

    #region properties

    public Gameboard Gameboard { get => gameboard; }
    public Tile Tile { get => tile; }

    #endregion

    #region monobehaviour

    void Awake()
    {
        subscribers = new List<ITileObjectSubscriber>();
    }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
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

    public void Toggle()
    {
        if (gameboard != null && tile != null)
        {
            gameboard?.Toggle(tile);
            NotifySubscribers();
        }
        else
            throw new System.Exception("Could not connect TileObject to gameboard and/or tile");
    }

    public void Reset()
    {
        Toggle(false);
    }

    #endregion

    #region 

    private void Toggle(bool state)
    {
        if (gameboard != null && tile != null)
        {
            gameboard?.Toggle(tile, state);
            NotifySubscribers();
        }
        else
            throw new System.Exception("Could not connect TileObject to gameboard and/or tile");
    }

    #endregion

    #region OnClick methods

    private void TaskOnClick()
    {
        ToggleCommand toggleCommand = new ToggleCommand(this);
        CommandManager.Instance.AddCommand(toggleCommand);
        toggleCommand.Execute();
    }

    #endregion

    #region publisher methods

    public void Subscribe(ITileObjectSubscriber subscriber)
    {
        if (subscriber != null)
        {
            subscribers.Add(subscriber);
        }
    }

    public void UnSubscribe(ITileObjectSubscriber subscriber)
    {
        if (subscriber != null)
        {
            subscribers.Remove(subscriber);
        }
    }

    public void NotifySubscribers()
    {
        foreach (ITileObjectSubscriber subscriber in subscribers)
        {
            subscriber.Update(this);
        }
    }

    #endregion

    #region destruction

    private void OnDestroy()
    {
        gameboard = null;
        tile = null;
        button = null;
        subscribers = null;
    }

    #endregion
}
