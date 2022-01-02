using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameboardObject : MonoBehaviour
{
    #region fields
    private Gameboard gameboard;
    public GameObject tilePrefab;
    public Transform gameboardPanel;
    #endregion

    #region monobehaviour
    // Start is called before the first frame update
    void Start()
    {
        gameboard = new Gameboard();
        CreateBoardGUI();
    }
    #endregion

    #region public methods
    public void CreateBoardGUI()
    {
        Tile[,] tiles = gameboard.CreateBoard();
        for (int row = 0; row < gameboard.Size(); row++)
        {
            for (int col = 0; col < gameboard.Size(); col++)
            {
                GameObject tile = Instantiate(tilePrefab, gameboardPanel);
                TileObject tileObject = tile.GetComponent<TileObject>();
                tileObject.Gameboard = gameboard;
                tileObject.Tile = tiles[row, col];
                //tileObject.SetupCommands();
            }
        }
    }

    public void SetDifficulty(int difficulty)
    {
        Debug.Log("Set difficulty to " + (Difficulties)difficulty);
        gameboard = new Gameboard(new Difficulty( (Difficulties) difficulty));
    }

    public void Reroll()
    {
        Debug.Log("Rerolling Board...");
        gameboard.GenerateSolution();
        gameboard.PrintSolution();
    }
    #endregion

}
