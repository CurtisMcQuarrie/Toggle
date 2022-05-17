using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* TileObject
 * Purpose:
 *      Allows the player to change the board state.
 *      Acts as a vessel to the Gameboard and Tile datastructures.
 * Remarks:
 *      Attached to each Button on the gameboard.
 *      Implements an Observer Pattern whenever the button it is attached to is pressed.
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
        // retrieve button and set the onClick method
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    #endregion

    #region interface

    /* ConnectTile
     * Purpose:
     *      Connects the "model" layer to the TileObject ("ViewModel" layer).
     *      Needs to be called whenever instantiating a TileObject_Prefab.
     * Params:
     *      Gameboard gameboard         The active gameboard being used in the scene.
     *      Tile tile                   A Tile within the active gameboard that will be 
     *                                  attached to this Gameboard.
     */
    public void ConnectTile(Gameboard gameboard, Tile tile)
    {
        if (gameboard != null && tile != null)
        {
            this.gameboard = gameboard;
            this.tile = tile;
        }
        else
        {
            throw new System.Exception("Could not connect TileObject to gameboard and/or tile");
        }
    }

    /* Toggle
     * Purpose:
     *      Changes the state of the Tile.
     *      Notifies all subscribers of this state change so that they can appropriately react.
     */
    public void Toggle()
    {
        if (gameboard != null && tile != null)
        {
            gameboard.Toggle(tile);
            NotifySubscribers();
        }
        else
            throw new System.Exception("Could not connect TileObject to gameboard and/or tile");
    }

    /* Reset
     * Purpose:
     *      Changes the state of the Tile to false.
     *      Notifies all subscribers of this state change so that they can appropriately react.
     */
    public void Reset()
    {
        if (gameboard != null && tile != null)
        {
            gameboard.Toggle(tile, false);
            NotifySubscribers();
        }
        else
        {
            throw new System.Exception("Could not connect TileObject to gameboard and/or tile");
        }
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

    /* Subscribe
     * Purpose: 
     *      Adds a subscriber to it's list so that it will be notified when
     *      a notable change occurs to the GameObject that this script is attached too.
     * Params:
     *      ITileObjectSubscriber subscriber        The object to add to the list of subscribers.
     */
    public void Subscribe(ITileObjectSubscriber subscriber)
    {
        // ensure that the subscriber is not null.
        if (subscriber != null)
        {
            subscribers.Add(subscriber);
        }
    }

    /* UnSubscribe
     * Purpose: 
     *      Removes the specified subscriber from it's list.
     * Params:
     *      ITileObjectSubscriber subscriber        The object to remove from the list of subscribers.
     */
    public void UnSubscribe(ITileObjectSubscriber subscriber)
    {
        // ensure that the subscriber is not null.
        if (subscriber != null)
        {
            subscribers.Remove(subscriber);
        }
    }

    /* NotifySubscribers
     * Purpose: 
     *      Notifies all subscribers in the subscriber list so that each will react to the notable
     *      state change.
     */
    public void NotifySubscribers()
    {
        // loop through each subscriber in the list
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
