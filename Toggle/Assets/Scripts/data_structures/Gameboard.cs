using UnityEngine;
using System.Collections.Generic;

public class Gameboard
{
    #region fields
    private int[,] solutions;
    private Tile[,] gameboard;
    private Tile[,] solutionBoard; // temp
    private List<Hint> rowHints;
    private List<Hint> colHints;
    private Difficulty currDifficulty;

    #endregion

    #region constructors
    public Gameboard()
    {
        currDifficulty = new Difficulty(Difficulties.EASY);
        rowHints = new List<Hint>();
        colHints = new List<Hint>();
    }

    public Gameboard(Difficulty difficulty)
    {
        currDifficulty = difficulty;
        rowHints = new List<Hint>();
        colHints = new List<Hint>();
    }
    #endregion

    #region properties
    public int Size() { return currDifficulty.BoardSize; }
    public Tile GetTile(int row, int col) { return gameboard[row, col]; }
    public int[] GetRowHints(int row) { return rowHints[row].HintValues; }
    public int[] GetColHints(int col) { return colHints[col].HintValues; }
    #endregion

    #region public methods
    public void ClearBoard()
    {
        if (gameboard != null)
        {
            for (int col = 0; col < currDifficulty.BoardSize; col++)
            {
                for (int row = 0; row < currDifficulty.BoardSize; row++)
                {
                    gameboard[row, col].Enabled = false;
                }
            }
        }
    }

    public bool CheckSolution()
    {
        bool solved = false;
        if (CheckRowSolution() && CheckColSolution())
        {
            solved = true;
        }
        return solved;
    }

    public Tile[,] CreateBoard()
    {
        Tile[,] board = new Tile[currDifficulty.BoardSize, currDifficulty.BoardSize];

        for (int col = 0; col < currDifficulty.BoardSize; col++)
        {
            for (int row = 0; row < currDifficulty.BoardSize; row++)
            {
                board[row, col] = new Tile();
            }
        }
        return board;
    }

    public void GenerateSolution()
    {
        gameboard = CreateBoard();
        solutionBoard = CreateBoard();
        ComputeEnabledCounts();
    }

    public Tile[] GetRow(int rowNum)
    {
        Tile[] rowTiles = new Tile[Size()];
        for (int col = 0; col < Size(); col++)
        {
            rowTiles[col] = gameboard[rowNum, col];
        }
        return rowTiles;
    }

    public Tile[] GetCol(int colNum)
    {
        Tile[] colTiles = new Tile[Size()];
        for (int row = 0; row < Size(); row++)
        {
            colTiles[row] = gameboard[row, colNum];
        }
        return colTiles;
    }

    public void PrintSolution()
    {
        string solution = "";
        for (int row = 0; row < currDifficulty.BoardSize; row++)
        {
            for (int col = 0; col < currDifficulty.BoardSize; col++)
            {
                solution += solutionBoard[row, col].ToString();
                if (col != currDifficulty.BoardSize - 1)
                    solution += ", ";
            }
            solution += "\n";
        }
        Debug.Log(solution);
    }

    public void Toggle(Tile tile)
    {
        tile.Toggle();
    }
    #endregion

    #region private methods

    #region solution helper methods
    // TODO
    private bool CheckColSolution()
    {
        bool solved = true;
        List<int> colCounts = new List<int>();
        for (int col = 0; col < currDifficulty.BoardSize; col++)
        {
            for (int row = 0; row < currDifficulty.BoardSize; row++)
            {

            }
        }
        return solved;
    }
    // TODO
    private bool CheckRowSolution()
    {
        bool solved = true;
        /*for (int col = 0; col < currDifficulty.BoardSize; col++)
        {
            for (int row = 0; row < currDifficulty.BoardSize; row++)
            {

            }
        }*/
        return solved;
    }

    /*
     * Computes the column hints
     * Uses the output from ComputeRowEnabledCount() to determine the column hints.
     */ 
    private void ComputeColEnabledCount()
    {
        for (int col = 0; col < currDifficulty.BoardSize; col++)
        {
            int consecutiveEnabled = 0;
            int currIndex = 0;
            colHints.Add(new Hint());

            for (int row = 0; row < currDifficulty.BoardSize; row++)
            {
                if (solutionBoard[row, col].Enabled)
                {
                    consecutiveEnabled++;
                }
                else if (consecutiveEnabled >= 1)
                {
                    colHints[col].Add(consecutiveEnabled);
                    consecutiveEnabled = 0;
                    currIndex++;
                }
            }
        }
    }

    /*
     * Generates the solution to the current gameboard.
     */ 
    private void ComputeEnabledCounts()
    {
        ComputeRowEnabledCount();
        ComputeColEnabledCount();
    }

    /*
     * Randomly generates the number of rows enabled in the solution.
     * Creates the row hints.
     */ 
    private void ComputeRowEnabledCount()
    {
        // randomly generate rowTileEnabledCount
        for (int row = 0; row < currDifficulty.BoardSize; row++)
        {
            bool filled = false;
            int startingCol = 0;
            int maxEnabled = currDifficulty.BoardSize + 1;
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