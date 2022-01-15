using System;
using System.Collections.Generic;
using UnityEngine;

/* GameboardController
 * Purpose:
 *      Uses GameboardGUI to instantiate gameboard prefabs and then connects them to the logic. 
 */
[RequireComponent(typeof(GameboardGUI))]
public class GameboardController : MonoBehaviour
{
    #region fields

    private List<GameObject> rowHints;
    private List<GameObject> columnHints;
    //private List<List<GameObject>> tileRow; //TODO: store tiles as GameObjects

    private Gameboard gameboard;
    public Transform gameboardTransform;
    private GameManager gameManager;
    private GameboardGUI gui;

    #endregion

    #region monobehaviour
    void Awake()
    {
        gameboard = new Gameboard(); // initializes gameboard
        rowHints = new List<GameObject>();
        columnHints = new List<GameObject>();
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gui = GetComponent<GameboardGUI>();
        gameboard.ChangeDifficulty(gameManager.difficulty);
        gameboard.PrintSolution();
        Initialize();
    }
    #endregion

    #region interface
    // instantiates board gui and connects Tile instances to tile prefabs
    public void Initialize()
    {
        gui.CreateSpacer(gameboardTransform); // instantiate spacer panel

        for (int col = 0; col < gameboard.Size; col++) // instantiate col hints
        {
            GameObject hint = gui.CreateHint(gameboard.GetColumnHints(col), IndexType.Column, gameboardTransform);
            columnHints.Add(hint);
        }

        // instantiate row hints and tiles simultaneously
        for (int row = 0; row < gameboard.Size; row++)
        {
            rowHints.Add(gui.CreateHint(gameboard.GetRowHints(row), IndexType.Row, gameboardTransform));
            for (int col = 0; col < gameboard.Size; col++)
            {
                GameObject tile = gui.CreateTile(gameboardTransform);
                TileObject tileObject = tile.GetComponent<TileObject>();
                tileObject.ConnectTile(gameboard, gameboard.GetTile(row, col)); // link gameobject to data structures
                // connect subscribers attached to gameobject
                ITileObjectSubscriber[] subscribers = tile.GetComponents<ITileObjectSubscriber>();
                foreach (ITileObjectSubscriber subscriber in subscribers)
                {
                    tileObject.Subscribe(subscriber);
                }
            }
        }
    }

    // sets the difficulty
    public void ChangeDifficulty(Difficulty difficulty)
    {
        Debug.Log("Set difficulty to " + difficulty);
        if (gameManager.difficulty != difficulty)
        {
            gameManager.difficulty = difficulty;
            gameboard.ChangeDifficulty(difficulty); // change gameboard difficulty
            UpdateGUI();
        }
    }


    /* Reroll
     * 
     */
    public void Reroll()
    {
        Debug.Log("Rerolling Board...");
        gameboard.Reroll();
        UpdateGUI();
        gameboard.PrintSolution();
    }


    public void Clear()
    {
        gameboard.Reset();
        ClearGUI();
    }
    #endregion

    #region gui

    // assumes the datastructure has already been updated.
    private void UpdateGUI()
    {
        //clear gui
        // cycle through tiles
        //tileObject.Toggle();
        //update hints
        // cycle through rowHints and columnHints
        //gui.UpdateHint();
    }

    private void ClearGUI()
    {
        
    }

    #endregion

    #region destruction
    private void OnDestroy()
    {
        
    }
    #endregion
}