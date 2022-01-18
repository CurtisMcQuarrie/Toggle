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
    private List<TileRow> tileRows;
    private GameObject spacer;

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
        tileRows = new List<TileRow>();
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
        spacer = gui.CreateSpacer(gameboardTransform); // instantiate spacer panel

        for (int col = 0; col < gameboard.Size; col++) // instantiate col hints
        {
            GameObject hint = gui.CreateHint(gameboard.GetColumnHints(col), IndexType.Column, gameboardTransform);
            columnHints.Add(hint);
        }

        // instantiate row hints and tiles simultaneously
        for (int row = 0; row < gameboard.Size; row++)
        {
            tileRows.Add(new TileRow());
            rowHints.Add(gui.CreateHint(gameboard.GetRowHints(row), IndexType.Row, gameboardTransform));
            for (int col = 0; col < gameboard.Size; col++)
            {
                GameObject tile = gui.CreateTile(gameboardTransform);
                TileObject tileObject = tile.GetComponent<TileObject>();
                tileObject.ConnectTile(gameboard, gameboard.GetTile(row, col)); // link gameobject to data structures
                tileRows[row].Add(tileObject);
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
    // TODO: remove
    public void ChangeDifficulty(int newDifficulty)
    {
        Difficulty difficulty = (Difficulty)newDifficulty;
        if (gameManager.difficulty != difficulty)
        {
            Debug.Log("Setting difficulty to " + difficulty);
            gameManager.difficulty = difficulty;
            ResetBoardGUI();
            gameboard.ChangeDifficulty(difficulty); // change gameboard difficulty
            //UpdateLists(oldDifficulty, (int)difficulty);// TODO: add/remove hints and tiles
            Initialize();
            //UpdateGUI();
        }
    }

    /* Reroll
     * Purpose:
     *      To refresh the gameboard state, clear the gui and generate a new solution.
     *      Difficulty is not changed.
     */
    public void Reroll()
    {
        Debug.Log("Rerolling Board...");
        gameboard.Reroll();
        UpdateGUI();
        gameboard.PrintSolution();
    }

    /* Clear
     * Purpose:
     *      Resets the gameboard state and the board GUI.
     *      Does not create a new solution.
     */ 
    public void Clear()
    {
        gameboard.Clear();
        ClearBoardGUI();
    }
    #endregion

    #region gui

    // assumes the datastructure has already been updated.
    private void UpdateGUI()
    {
        ClearBoardGUI();
        //update hints
        for (int index = 0; index < rowHints.Count; index++) // cycle through rowHints and columnHints
        {
            gui.UpdateHint(gameboard.GetRowHints(index), IndexType.Row, rowHints[index]);
            gui.UpdateHint(gameboard.GetColumnHints(index), IndexType.Column, columnHints[index]);
        }
        CommandManager commandManager = CommandManager.Instance;
        commandManager.Reset(); // reset commandBuffer (prevent undo)
    }

    /* ClearGUI
     * Purpose:
     *      Cycle through tile objects and Reset them.
     */ 
    private void ClearBoardGUI()
    {
        for (int index = 0; index < gameboard.Size; index++)
        {
            tileRows[index].Reset();
        }
    }

    // TODO
    private void ResetBoardGUI()
    {
        //destroy all board objects
        for (int row = 0; row < gameboard.Size; row++)
        {
            gui.DestroyObject(rowHints[row].gameObject);
            gui.DestroyObject(columnHints[row].gameObject);
            for (int col = 0; col < gameboard.Size; col++)
            {
                gui.DestroyObject(tileRows[row].GetTileObject(col).gameObject);
            }
        }
        // clear lists
        rowHints.Clear();
        columnHints.Clear();
        tileRows.Clear();
        gui.DestroyObject(spacer);
        spacer = null;
        CommandManager commandManager = CommandManager.Instance;
        commandManager.Reset(); // reset commandBuffer (prevent undo)
    }

    #endregion

    #region destruction
    private void OnDestroy()
    {
        rowHints.Clear();
        columnHints.Clear();
        tileRows.Clear();
        gameboard.Destroy(); // important part
        gameboardTransform = null;
        gameManager = null;
        gui = null;
    }
    #endregion
}