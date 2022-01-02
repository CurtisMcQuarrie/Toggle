using UnityEngine;
using System.Collections.Generic;

public class Gameboard
{
    #region fields
    private int[,] solutions;
    private Tile[,] gameboard;
    private Tile[,] solutionBoard; // temp
    private List<int> rowTileEnabledCount;
    private List<int> colTileEnabledCount;
    private Difficulty currDifficulty;
    #endregion

    #region constructors
    public Gameboard()
    {
        currDifficulty = new Difficulty(Difficulties.EASY);
        //gameboard = CreateBoard();
        rowTileEnabledCount = new List<int>();
        colTileEnabledCount = new List<int>();
    }

    public Gameboard(Difficulty difficulty)
    {
        currDifficulty = difficulty;
        //gameboard = CreateBoard();
        rowTileEnabledCount = new List<int>();
        colTileEnabledCount = new List<int>();
    }
    #endregion

    #region properties
    public int Size() { return currDifficulty.BoardSize; }
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
        solutionBoard = CreateBoard();
        ComputeEnabledCounts();
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

    private void ComputeColEnabledCount()
    {
        for (int col = 0; col < currDifficulty.BoardSize; col++)
        {
            int consecutiveEnabled = 0;
            int currIndex = 0;
            
            for (int row = 0; row < currDifficulty.BoardSize; row++)
            {
                if (solutionBoard[row, col].Enabled)
                {
                    consecutiveEnabled++;
                }
                else if (consecutiveEnabled >= 1)
                {
                    colTileEnabledCount.Add(consecutiveEnabled); //might need to change to 2D
                    consecutiveEnabled = 0;
                    currIndex++;
                }
            }
        }
    }

    private void ComputeEnabledCounts()
    {
        ComputeRowEnabledCount();
        ComputeColEnabledCount();
    }

    private void ComputeRowEnabledCount()
    {
        // randomly generate rowTileEnabledCount
        for (int row = 0; row < currDifficulty.BoardSize; row++)
        {
            bool rowFilled = false;
            int startingCol = 0;
            int maxEnabled = currDifficulty.BoardSize + 1;
            while (!rowFilled)
            {
                // logic for randomly generating row tile count enabled
                int amountEnabled = Random.Range(0, maxEnabled);
                rowTileEnabledCount.Add(amountEnabled); //might need to change to 2D
                maxEnabled -= (amountEnabled + 1);
                if (maxEnabled < 1 || amountEnabled <= 0)
                {
                    rowFilled = true;
                }
                // TODO: add a random startingCol to place
                EnableSolutionTileRows(row, startingCol, amountEnabled);
                startingCol += amountEnabled + 1;
            }
        }
    }

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