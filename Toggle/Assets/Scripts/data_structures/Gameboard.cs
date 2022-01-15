using UnityEngine;
using System.Collections.Generic;
using System.Text;

/* Gameboard
 * Purpose:
 *      Holds basic details about the current game.
 *      Handles Tile instantiation, manipulation and destruction for both the solution board and the gameboard.
 *      Computes hints that are used to solve the game.
 */ 
public class Gameboard
{
    #region constants

    private const int initialSize = 3;

    #endregion

    #region fields

    private Tile[,] gameboard; // TODO: change to list
    private List<List<Tile>> gameboardList;
    private Tile[,] solutionBoard; // TODO: change to list
    private List<List<Tile>> solutionBoardList;
    private List<Hint> rowHints;
    private List<Hint> columnHints;
    private int size;

    #endregion

    #region constructors

    public Gameboard()
    {
        GenerateBoard(Difficulty.Easy);
    }

    public Gameboard(Difficulty difficulty)
    {
        GenerateBoard(difficulty);
    }

    #endregion

    #region properties, getters and setters

    public Difficulty GetDifficulty() { return (Difficulty)(size - initialSize); }
    public int Size() { return size; }

    public int[] GetColumnHints(int column) { return columnHints[column].HintValues; }
    public int[] GetRowHints(int row) { return rowHints[row].HintValues; }
    
    public Tile GetTile(int row, int column) { return gameboard[row, column]; }
    /* GetTiles
     * Purpose:
     *      Retrieves the Tile instances stored in the specified row or column number.
     * Params:
     *      IndexType indexType     Specifies whether to retrieve row or column.
     *      int indexNumber         Specifies row or column number to fetch the Tile instances from.
     * Returns: 
     *      Tile[] rowTiles         All the the Tile instances stored in the specified column number. 
     */
    public Tile[] GetTiles(IndexType indexType, int indexNumber)
    {
        Tile[] tiles = new Tile[size];
        int rowIndex = 0;
        int colIndex = 0;
        
        for (int index = 0; index < size; index++)
        {
            if (indexType == IndexType.Row)
            {
                rowIndex = indexNumber;
                colIndex = index;
            }
            else if (indexType == IndexType.Column)
            {
                rowIndex = index;
                colIndex = indexNumber;
            }
            else
                throw new System.Exception("Unspecified index type.");

            tiles[index] = gameboard[rowIndex, colIndex];
        }
        return tiles;
    }
    #endregion
    
    #region interface

    /* GenerateBoard
     * Purpose:
     *      Sets up a new game to play.
     *      Initializes the gameboard and solution board.
     *      Generates the solution to the game.
     *      Computes the hints for the game.
     */
    private void GenerateBoard(Difficulty difficulty)
    {
        InitializeBoards(difficulty);
        GenerateSolution();
        GenerateHints(IndexType.Row);
        GenerateHints(IndexType.Column);
    }
    
    public void Toggle(Tile tile)
    {
        tile.Toggle();
        // everytime a Tile is toggled, you want to check the win condition
        // TODO: move solution checking to GameboardController
        if (CheckSolution())
            Debug.Log("You Win!");
    }
    
    /* ClearBoard
     * Purpose:
     *      Sets all Tiles within the gameboard field to isOn=false.
     */ 
    public void ClearBoard()
    {
        if (gameboard != null)
        {
            //new code
            for (int row = 0; row < size; row++)
            {
                List<Tile> currRow = gameboardList[row];
                for (int col = 0; col < size; col++)
                {
                    currRow[col]?.Reset();
                }
            }
            //end new code
            //old code
            for (int col = 0; col < size; col++)
            {
                for (int row = 0; row < size; row++)
                {
                    gameboard[row, col]?.Reset();
                }
            }
            //end old code
        }
        else
        {
            Debug.Log("When trying to clear the board, Gameboard instance was null.");
        }
    }

    public void PrintSolution()
    {
        StringBuilder solution = new StringBuilder();
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                solution.Append(solutionBoard[row, col].ToString());
                if (col != size - 1)
                    solution.Append(", ");
            }
            solution.Append("\n");
        }
        Debug.Log(solution.ToString());
    }

    //new board size
    public void ChangeDifficulty(Difficulty difficulty)
    {
        // determine if new size is smaller or larger
        int oldSize = size;
        size = (int)difficulty + initialSize;
        if (oldSize < size)
        {

        }
        else
        {

        }
    }

    //same board size
    public void Reroll()
    {
        // clear gameboard
        // clear solutionboard
        // destroy current hints
        // need to generate new hints
    }

    #endregion
    
    #region initialization

    /* InitializeBoard
     * Purpose:
     *      Initializes class fields.
     * Params:
     *      Difficulty difficulty
     *          Used to determine board size.
     */ 
    private void InitializeBoards(Difficulty difficulty)
    {
        rowHints = new List<Hint>();
        columnHints = new List<Hint>();
        size = (int)difficulty + initialSize;
        gameboard = CreateBoard();
        solutionBoard = CreateBoard();
    }
    
    /* CreateBoard
     * Purpose:
     *      Initializes 2D Tile array and returns result.
     * Returns: 
     *      Tile[,] board       a 2D array that stores initiated Tile objects.
     */ 
    private Tile[,] CreateBoard()
    {
        Tile[,] board = new Tile[size, size];

        for (int col = 0; col < size; col++)
        {
            for (int row = 0; row < size; row++)
            {
                board[row, col] = new Tile();
            }
        }
        return board;
    }

    #endregion
    
    #region solution generation

    /* GenerateSolution
     * Purpose:
     *      Randomly generates the number of rows enabled in the solutionBoard.
     *      Randomizes by rows.
     */
    private void GenerateSolution()
    {
        for (int row = 0; row < size; row++)
        {
            bool filled = false;
            int startingCol = 0;
            int maxEnabled = size + 1;
            while (!filled)
            {
                // logic for randomly generating row tile count enabled
                int amountEnabled = Random.Range(0, maxEnabled);
                maxEnabled -= (amountEnabled + 1);
                if (maxEnabled < 1 || amountEnabled <= 0)
                {
                    filled = true;
                }

                // TODO: add a random startingCol to place

                ToggleSolutionBoardRow(row, startingCol, amountEnabled);
                startingCol += amountEnabled + 1;
            }
        }
    }

    /* ToggleSolutionBoardRow
     * Purpose:
     *      Solves the solution gameboard which is used to determine the column hints.
     */
    private void ToggleSolutionBoardRow(int rowIndex, int colIndex, int amountEnabled)
    {
        for (int index = 0; index < amountEnabled; index++)
        {
            solutionBoard[rowIndex, colIndex + index].Toggle();
        }
    }

    #endregion

    #region hints generation

    /* GenerateHints
     * Purpose:
     *      To compute the hints for the specified List in the game.
     * Params:
     *      IndexType indexType     Specifies whether row hints or column hints are being computed.
     */ 
    private void GenerateHints(IndexType indexType)
    {
        int rowIndex = 0;
        int columnIndex = 0;
        List<Hint> hintList = null;

        // determines the hint list to manipulate
        if (indexType == IndexType.Row)
            hintList = rowHints;
        else if (indexType == IndexType.Column)
            hintList = columnHints;
        else
            throw new System.Exception("Unspecified index type.");

        for (int outerIndex = 0; outerIndex < size; outerIndex++)
        {
            int consecutiveIsOn = 0;
            hintList.Add(new Hint());

            for (int innerIndex = 0; innerIndex < size; innerIndex++)
            {
                // rowIndex and columnIndex change depending on the indexType
                if (indexType == IndexType.Row)
                {
                    rowIndex = outerIndex;
                    columnIndex = innerIndex;
                }
                else
                {
                    rowIndex = innerIndex;
                    columnIndex = outerIndex;
                }
                // cases for computing hints
                if (solutionBoard[rowIndex, columnIndex].IsOn)
                {
                    consecutiveIsOn++;
                    if (innerIndex == size - 1)
                    {
                        hintList[outerIndex].Add(consecutiveIsOn);
                    }
                }
                else if (consecutiveIsOn >= 1)
                {
                    hintList[outerIndex].Add(consecutiveIsOn);
                    consecutiveIsOn = 0;
                }
            }
        }
    }

    #endregion

    #region solution checking

    private bool CheckSolution()
    {
        return CheckSolution(IndexType.Row) && CheckSolution(IndexType.Column);
    }

    /* CheckSolution
     * Purpose:
     *      Checks whether or not all columns or rows match the hints.
     * Params:
     *      IndexType indexType     Determines whether the row or column hints are being checked.
     * Returns:
     *      bool solved             True if all Tile isOn fields match the hints.
     */ 
    private bool CheckSolution(IndexType indexType)
    {
        bool solved = true;
        int[] currHint = null;
        int rowIndex = 0;
        int colIndex = 0;

        for (int outerIndex = 0; outerIndex < size && solved; outerIndex++)
        {
            int currHintIndex = 0;
            int consecutiveIsOn = 0;

            if(indexType == IndexType.Row)
                currHint = GetRowHints(outerIndex);
            else if (indexType == IndexType.Column)
                currHint = GetColumnHints(outerIndex);
            else
                throw new System.Exception("Unspecified index type.");

            for (int innerIndex = 0; innerIndex < size && solved; innerIndex++)
            {
                if (indexType == IndexType.Row)
                {
                    rowIndex = outerIndex;
                    colIndex = innerIndex;
                }
                else
                {
                    rowIndex = innerIndex;
                    colIndex = outerIndex;
                }

                if (gameboard[rowIndex, colIndex].IsOn)
                {
                    consecutiveIsOn++;
                    if (currHint.Length == 0 || currHintIndex >= currHint.Length)
                    {
                        solved = false;
                    }
                    else if (innerIndex == size - 1 && (currHintIndex != currHint.Length - 1 || consecutiveIsOn != currHint[currHintIndex]))
                    {
                        solved = false;
                    }
                }
                else if (!gameboard[rowIndex, colIndex].IsOn)
                {
                    if (consecutiveIsOn > 0)
                    {
                        if (currHint.Length == 0 || currHintIndex >= currHint.Length)
                        {
                            solved = false;
                        }
                        else
                        {
                            if (innerIndex == size - 1 && (currHintIndex != currHint.Length - 1 || consecutiveIsOn != currHint[currHintIndex]))
                            {
                                solved = false;
                            }
                            else if (consecutiveIsOn != currHint[currHintIndex])
                            {
                                solved = false;
                            }
                            else if (consecutiveIsOn == currHint[currHintIndex])
                            {
                                currHintIndex++;
                                consecutiveIsOn = 0;
                            }
                        }
                    }
                    else if (innerIndex == size - 1 && currHintIndex != currHint.Length)
                    {
                        solved = false;
                    }
                }
            }

        }

        return solved;
    }

    #endregion

    #region destruction

    public void Destroy()
    {
        // safely clear the lists in the Hints objects
        for (int index = 0; index < size; index++)
        {
            rowHints[index].Clear();
            columnHints[index].Clear();
        }
        rowHints = null;
        columnHints = null;
        gameboard = null;
        solutionBoard = null;
    }

    #endregion

}

public enum Difficulty { Easy, Medium, Hard }

public enum IndexType { Row, Column }