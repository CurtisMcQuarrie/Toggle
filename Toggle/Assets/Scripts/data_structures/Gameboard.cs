using UnityEngine;
using System.Collections.Generic;

public class Gameboard
{
    #region constants
    private const int initialSize = 3;
    #endregion

    #region fields
    private Tile[,] gameboard;
    private Tile[,] solutionBoard;
    private List<Hint> rowHints;
    private List<Hint> colHints;
    private int size;
    #endregion

    #region constructors
    public Gameboard()
    {
        rowHints = new List<Hint>();
        colHints = new List<Hint>();
        size = (int)Difficulty.Easy + initialSize;
    }

    public Gameboard(Difficulty difficulty)
    {
        rowHints = new List<Hint>();
        colHints = new List<Hint>();
        size = (int)difficulty + initialSize;
    }
    #endregion

    #region properties
    public int Size() { return size; }
    public Tile GetTile(int row, int col) { return gameboard[row, col]; }
    public int[] GetRowHints(int row) { return rowHints[row].HintValues; }
    public int[] GetColHints(int col) { return colHints[col].HintValues; }
    #endregion

    #region public methods
    public void ClearBoard()
    {
        if (gameboard != null)
        {
            for (int col = 0; col < size; col++)
            {
                for (int row = 0; row < size; row++)
                {
                    if (gameboard[row, col].IsOn == true)
                        gameboard[row, col].Toggle();
                }
            }
        }
    }

    public Tile[,] CreateBoard()
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

    public void GenerateSolution()
    {
        Debug.Log("Generating solution for board...");
        gameboard = CreateBoard();
        solutionBoard = CreateBoard();
        ComputeEnabledCounts();
        Debug.Log("Solution generation completed.");
    }

    public Tile[] GetRow(int rowNum)
    {
        Tile[] rowTiles = new Tile[size];
        for (int col = 0; col < size; col++)
        {
            rowTiles[col] = gameboard[rowNum, col];
        }
        return rowTiles;
    }

    public Tile[] GetCol(int colNum)
    {
        Tile[] colTiles = new Tile[size];
        for (int row = 0; row < size; row++)
        {
            colTiles[row] = gameboard[row, colNum];
        }
        return colTiles;
    }

    public void PrintSolution()
    {
        string solution = "";
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                solution += solutionBoard[row, col].ToString();
                if (col != size - 1)
                    solution += ", ";
            }
            solution += "\n";
        }
        Debug.Log(solution);
    }

    public void ToggleTile(Tile tile)
    {
        tile.Toggle();
        if (CheckSolution())
            Debug.Log("You Win!");
    }
    #endregion

    #region private methods

    #region solution checking helper methods

    private bool CheckSolution()
    {
        return CheckColSolution() && CheckRowSolution();
    }
    
    private bool CheckColSolution()
    {
        bool solved = true;
        for (int col = 0; col < size && solved; col++)
        {
            int[] currColHint = GetColHints(col);
            int currHintIndex = 0;
            int consecutiveEnabled = 0;
            for (int row = 0; row < size && solved; row++)
            {
                if (gameboard[row, col].IsOn)
                {
                    consecutiveEnabled++;
                    if (currColHint.Length == 0 || currHintIndex >= currColHint.Length)
                    {
                        solved = false;
                    }
                    else if (row == size - 1 && (currHintIndex != currColHint.Length - 1 || consecutiveEnabled != currColHint[currHintIndex]))
                    {
                        solved = false;
                    }
                }
                else if (!gameboard[row, col].IsOn)
                {
                    if (consecutiveEnabled > 0)
                    {
                        if (currColHint.Length == 0 || currHintIndex >= currColHint.Length)
                        {
                            solved = false;
                        }
                        else
                        {
                            if (row == size - 1 && (currHintIndex != currColHint.Length - 1 || consecutiveEnabled != currColHint[currHintIndex]))
                            {
                                solved = false;
                            }
                            else if (consecutiveEnabled != currColHint[currHintIndex])
                            {
                                solved = false;
                            }
                            else if (consecutiveEnabled == currColHint[currHintIndex])
                            {
                                currHintIndex++;
                                consecutiveEnabled = 0;
                            }
                        }
                    }
                    else if (row == size - 1 && currHintIndex != currColHint.Length)
                    {
                        solved = false;
                    }
                }
            }
        }
        return solved;
    }

    private bool CheckRowSolution()
    {
        bool solved = true;
        for (int row = 0; row < size && solved; row++)
        {
            int[] currRowHint = GetRowHints(row);
            int currHintIndex = 0;
            int consecutiveEnabled = 0;
            for (int col = 0; col < size && solved; col++)
            {
                if (gameboard[row, col].IsOn)
                {
                    consecutiveEnabled++;
                    if (currRowHint.Length == 0 || currHintIndex >= currRowHint.Length)
                    {
                        solved = false;
                    }
                    else if (col == size - 1 && (currHintIndex != currRowHint.Length - 1 || consecutiveEnabled != currRowHint[currHintIndex]))
                    {
                        solved = false;
                    }
                }
                else if (!gameboard[row, col].IsOn)
                {
                    if (consecutiveEnabled > 0)
                    {
                        if (currRowHint.Length == 0 || currHintIndex >= currRowHint.Length)
                        {
                            solved = false;
                        }
                        else
                        {
                            if (col == size - 1 && (currHintIndex != currRowHint.Length - 1 || consecutiveEnabled != currRowHint[currHintIndex]))
                            {
                                solved = false;
                            }
                            else if (consecutiveEnabled != currRowHint[currHintIndex])
                            {
                                solved = false;
                            }
                            else if (consecutiveEnabled == currRowHint[currHintIndex])
                            {
                                currHintIndex++;
                                consecutiveEnabled = 0;
                            }
                        }
                    }
                    else if (col == size - 1 && currHintIndex != currRowHint.Length)
                    {
                        solved = false;
                    }
                }
            }
        }
        return solved;
    }
    #endregion

    #region solution generation helper methods
    /*
     * Computes the column hints
     * Uses the output from ComputeRowEnabledCount() to determine the column hints.
     */
    private void ComputeColEnabledCount()
    {
        for (int col = 0; col < size; col++)
        {
            int consecutiveEnabled = 0;
            colHints.Add(new Hint());

            for (int row = 0; row < size; row++)
            {
                if (solutionBoard[row, col].IsOn)
                {
                    consecutiveEnabled++;
                    if (row == size - 1)
                    {
                        colHints[col].Add(consecutiveEnabled);
                    }
                }
                else if (consecutiveEnabled >= 1)
                {
                    colHints[col].Add(consecutiveEnabled);
                    consecutiveEnabled = 0;
                }
            }
        }
    }

    /*
     * Generates the solution to the current gameboard.
     */ 
    private void ComputeEnabledCounts()
    {
        Debug.Log("Computing rows...");
        ComputeRowEnabledCount();
        Debug.Log("Row computation complete.");
        Debug.Log("Computing columns...");
        ComputeColEnabledCount();
        Debug.Log("Column computation complete.");
    }

    /*
     * Randomly generates the number of rows enabled in the solution.
     * Creates the row hints.
     */ 
    private void ComputeRowEnabledCount()
    {
        // randomly generate rowTileEnabledCount
        for (int row = 0; row < size; row++)
        {
            bool filled = false;
            int startingCol = 0;
            int maxEnabled = size + 1;
            rowHints.Add(new Hint());
            while (!filled)
            {
                // logic for randomly generating row tile count enabled
                int amountEnabled = Random.Range(0, maxEnabled);
                maxEnabled -= (amountEnabled + 1);
                if (maxEnabled < 1 || amountEnabled <= 0)
                {
                    filled = true;
                }
                if (amountEnabled > 0)
                {
                    rowHints[row].Add(amountEnabled);
                }
                
                // TODO: add a random startingCol to place

                EnableSolutionTileRows(row, startingCol, amountEnabled);
                startingCol += amountEnabled + 1;
            }
        }
    }

    /*
     * Solves the solution gameboard which is used to determine the column hints.
     */ 
    private void EnableSolutionTileRows(int rowIndex, int colIndex, int amountEnabled)
    {
        //Debug.Log("Current rowIndex is " + rowIndex + ", colIndex is " + colIndex + " and amountEnabled is " + amountEnabled);
        for (int index = 0; index < amountEnabled; index++)
        {
            solutionBoard[rowIndex, colIndex + index].Toggle();
        }
    }
    #endregion 

    #endregion
}

public enum Difficulty { Easy, Medium, Hard }