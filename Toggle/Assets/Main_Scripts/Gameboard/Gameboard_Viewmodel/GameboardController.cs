using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* GameboardController
 * Purpose:
 *      Uses GameboardGUI to instantiate gameboard prefabs and then connects them to the logic. 
 */
[RequireComponent(typeof(GameboardGUI))]
public class GameboardController : MonoBehaviour, ITileObjectSubscriber
{
    #region fields

    public float loadGameDelay = 0f;
    public SimpleTimer timer;

    private List<List<TileObject>> tileObjectList;

    private Gameboard gameboard;
    private GameManager gameManager;
    private GameboardGUI gui;

    #endregion

    #region monobehaviour
    void Awake()
    {
        // initialize private fields
        gameboard = new Gameboard();
        tileObjectList = new List<List<TileObject>>();
    }

    void Start()
    {
        // retrieve dependencies
        gameManager = FindObjectOfType<GameManager>();
        gui = GetComponent<GameboardGUI>();
    }
    #endregion

    #region interface

    /* Setup
     * Purpose: 
     *      Instantiates GUI components and attaches the appropriate scripts to them.
     */
    public void Setup()
    {
        gameboard.ChangeDifficulty(gameManager.difficulty); // initalize the gameboard to the specified difficulty
        
        tileObjectList = gui.CreateBoard(gameboard); // use the gameboard to create the GUI

        // generate the TileObjects and connect them to the created GUI gameobjects
        for (int row = 0; row < gameboard.Size; row++)
        {
            tileObjectList.Add(new List<TileObject>());
            for (int col = 0; col < gameboard.Size; col++)
            {
                TileObject tileObject = tileObjectList[row][col];
                tileObject.ConnectTile(gameboard, gameboard.GetTile(row, col));
                tileObject.Subscribe(this);
            }
        }
    }

    // sets the difficulty
    public void ChangeDifficulty(int newDifficulty)
    {
        timer.StopStopWatch();
        timer.ResetStopWatch();
        StartCoroutine(LoadGame(newDifficulty));
    }

    /* Clear
     * Purpose:
     *      Goes through each TileObject and resets their state to false.
     *      Does not create a new solution.
     */
    public void Clear()
    {
        for (int row = 0; row < gameboard.Size; row++)
        {
            for (int col = 0; col < gameboard.Size; col++)
            {
                tileObjectList[row][col].Reset();
            }
        }
    }

    /* Reroll
     * Purpose:
     *      To refresh the gameboard state, clear the gui and generate a new solution.
     *      Difficulty is not changed.
     */
    public void Reroll()
    {
        timer.StopStopWatch();
        timer.ResetStopWatch();
        StartCoroutine(LoadGame((int)gameManager.difficulty));
    }

    public void Reset()
    {
        gui.DestroyAll(); // destroy existing GUI gameobjects
        Setup(); // reinstantiate the GUI gameobjects
    }

    public void DisplayWinPanel(bool showPanel)
    {
        gui.DisplayWinPanel(showPanel);
    }

    public void DisplayGamePanel(bool showPanel)
    {
        gui.DisplayGamePanel(showPanel);
    }

    #region IEnumerators

    IEnumerator LoadGame(int newDifficulty)
    {
        Difficulty difficulty = (Difficulty)newDifficulty;

        gameManager.difficulty = difficulty; // update the global difficulty
        yield return new WaitForSeconds(loadGameDelay);
        Reset();
        timer.StartStopWatch();
    }

    #endregion

    #endregion

    #region destruction

    private void OnDestroy()
    {
        tileObjectList.Clear();

        gameboard.Destroy(); // important part

        gameManager = null;
        gui = null;
    }

    #endregion

    #region interface implementations

    void ITileObjectSubscriber.Update(TileObject tile)
    {
        if (gameboard.CheckSolution())
        {
            timer.StopStopWatch();
            timer.ResetStopWatch();
            gui.DisplayWinPanel(true);
            gui.DisplayGamePanel(false);
        }
    }

    void ISubscriber.Update()
    {
        throw new System.NotImplementedException();
    }
    
    #endregion
}