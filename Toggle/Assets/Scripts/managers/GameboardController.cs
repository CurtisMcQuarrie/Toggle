using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameboardController : MonoBehaviour
{
    #region fields
    private Gameboard gameboard;
    private HintsObject hintsObject;
    private GameboardObject gameboardObject;
    public Transform gameboardPanelTransform;
    #endregion

    #region monobehaviour
    void Start()
    {
        gameboard = new Gameboard();
        hintsObject = GetComponent<HintsObject>();
        gameboardObject = GetComponent<GameboardObject>();
        if (hintsObject == null)
        {
            Debug.Log("Missing HintsObject on Gameboard_Controller.");
        }
        else if (gameboardObject == null)
        {
            Debug.Log("Missing GameboardObject on Gameboard_Controller.");
        }
        CreateBoard();
    }
    #endregion

    #region methods
    public void CreateBoard()
    {
        gameboard.GenerateSolution();
        gameboardObject.CreateSpacerPanel(gameboardPanelTransform);

        // instantiate col hints
        for (int col = 0; col < gameboard.Size(); col++)
        {
            hintsObject.CreateHint(gameboard.GetColHints(col), HintsPrefabs.COL, gameboardPanelTransform);

            //Debug
            //int[] hints = gameboard.GetColHints(col);
            //Debug.Log("column " + col + " hint is:");
            //string hintString = "";
            //foreach (int hint in hints)
            //{
            //    hintString += hint + " ";
            //}
            //Debug.Log(hintString);

        }

        // instantiate row hints and tiles simultaneously
        for (int row = 0; row < gameboard.Size(); row++)
        {
            hintsObject.CreateHint(gameboard.GetRowHints(row), HintsPrefabs.ROW, gameboardPanelTransform); // instantiate hint for row
            GameObject[] rowTileObjects = gameboardObject.CreateBoardRow(gameboard.GetRow(row), gameboardPanelTransform); // instantiate tiles
            ConnectRowTileObjects(rowTileObjects, row); // hookup TileObject to gameboard
        }

        gameboard.PrintSolution();
    }

    public void SetDifficulty(int difficulty)
    {
        Debug.Log("Set difficulty to " + (Difficulty)difficulty);
        gameboard = new Gameboard((Difficulty)difficulty);
    }

    public void Reroll()
    {
        Debug.Log("Rerolling Board...");
        gameboard.GenerateSolution();
        gameboard.PrintSolution();
    }

    private void ConnectRowTileObjects(GameObject[] rowTileObjects, int rowNum)
    {
        for (int col = 0; col < rowTileObjects.Length; col++)
        {
            TileObject tileObject = rowTileObjects[col].GetComponent<TileObject>();
            tileObject.Gameboard = gameboard;
            tileObject.Tile = gameboard.GetTile(rowNum, col);
        }
    }
    #endregion
}