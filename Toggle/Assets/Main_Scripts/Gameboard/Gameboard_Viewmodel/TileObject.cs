using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Allows the player to change the board state.
/// Acts as a vessel to the Gameboard and Tile datastructures.
/// Attached to each Button on the gameboard.
/// Implements an Observer Pattern whenever the button it is attached to is pressed.
/// </summary>
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

    /// <summary>
    /// Connects the "model" layer to the TileObject ("ViewModel" layer).
    /// Needs to be called whenever instantiating a TileObject_Prefab.
    /// </summary>
    /// <param name="gameboard">The active gameboard being used in the scene.</param>
    /// <param name="tile">A Tile within the active gameboard that will be attached to this Gameboard.</param>
    public void ConnectTile(Gameboard gameboard, Tile tile)
    {
        if (gameboard != null && tile != null)
        {
            this.gameboard = gameboard;
            this.tile = tile;
            ConnectAttachedSubscribers();
        }
        else
        {
            throw new System.Exception("Could not connect TileObject to gameboard and/or tile");
        }
    }

    /// <summary>
    /// Changes the state of the Tile.
    /// Notifies all subscribers of this state change so that they can appropriately react.
    /// </summary>
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

    /// <summary>
    /// Changes the state of the Tile.
    /// Notifies all subscribers of this state change so that they can appropriately react.
    /// </summary>
    /// <param name="state">The new state to set the Tile too.</param>
    public void Toggle(bool state)
    {
        if (gameboard != null && tile != null)
        {
            gameboard.Toggle(tile, state);
            NotifySubscribers();
        }
        else
        {
            throw new System.Exception("Could not connect TileObject to gameboard and/or tile");
        }
    }

    /// <summary>
    /// Changes the state of the Tile to false.
    /// </summary>
    public void Reset()
    {
        Toggle(false);
    }

    #endregion

    #region on click methods

    /// <summary>
    /// Performs a Toggle method call when this button is clicked.
    /// </summary>
    private void TaskOnClick()
    {
        Toggle();
    }

    #endregion

    #region publisher methods

    /// <summary>
    /// Adds a subscriber to it's list so that it will be notified when a notable change occurs to 
    /// the GameObject that this script is attached too.
    /// </summary>
    /// <param name="subscriber">The object to add to the list of subscribers.</param>
    public void Subscribe(ITileObjectSubscriber subscriber)
    {
        // ensure that the subscriber is not null
        if (subscriber != null)
        {
            subscribers.Add(subscriber);
        }
    }

    /// <summary>
    /// Removes the specified subscriber from it's list.
    /// </summary>
    /// <param name="subscriber">The object to remove from the list of subscribers.</param>
    public void UnSubscribe(ITileObjectSubscriber subscriber)
    {
        // ensure that the subscriber is not null
        if (subscriber != null)
        {
            subscribers.Remove(subscriber);
        }
    }

     /// <summary>
     /// Notifies all subscribers in the subscriber list so that each will react to the notable state change.
     /// </summary>
    public void NotifySubscribers()
    {
        // loop through each subscriber in the list
        foreach (ITileObjectSubscriber subscriber in subscribers)
        {
            subscriber.Update(this);
        }
    }

    /// <summary>
    /// Subscribes the ITileObjectSubscriber scripts that are attached to this gameobject.
    /// </summary>
    private void ConnectAttachedSubscribers()
    {
        ITileObjectSubscriber[] subscribers = gameObject.GetComponents<ITileObjectSubscriber>();
        foreach (ITileObjectSubscriber subscriber in subscribers)
        {
            Subscribe(subscriber);
        }
    }

    #endregion

    #region destruction

    /// <summary>
    /// Removes all instantiated variables.
    /// </summary>
    private void OnDestroy()
    {
        gameboard = null;
        tile = null;
        button = null;
        subscribers = null;
    }

    #endregion
}
