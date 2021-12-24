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
        gameboard = CreateBoard();
        rowTileEnabledCount = new List<int>();
        colTileEnabledCount = new List<int>();
    }

    public Gameboard(Difficulty difficulty)
    {
        currDifficulty = difficulty;
        gameboard = CreateBoard();
        rowTileEnabledCount = new List<int>();
        colTileEnabledCount = new List<int>();
    }
    #endregion

    #region properties

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
    #endregion

    #region private methods

    #region solution helper methods
    private bool CheckColSolution()
    {
        bool solved = true;
        /*for (int col = 0; col < currDifficulty.BoardSize; col++)
        {
            int[] countEnabled = new int[currDifficulty.BoardSize];
            int startIndex = 0;
            for (int row = 0; row < currDifficulty.BoardSize; row++)
            {
                if (board[row,col].Enabled)
                {
                    countEnabled++;
                }

            }
            if (solution[row, col] != countEnabled)
            {
                solved = false;
                break;
            }
        }*/
        return solved;
    }

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
                    colTileEnabledCount.Add(consecutiveEnabled);
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
            //int currCountDisabled = currDifficulty.BoardSize;
            while (!rowFilled)
            {
                // logic for randomly generating row tile count enabled
                //int amountEnabled = Random.Range(0, currCountDisabled+1);
                int amountEnabled = Random.Range(0, maxEnabled);
                rowTileEnabledCount.Add(amountEnabled);
                //currCountDisabled -= (amountEnabled+1);
                maxEnabled -= (amountEnabled + 1);
                //if (currCountDisabled <= 1 || amountEnabled <= 0)
                if (maxEnabled < 1 || amountEnabled <= 0)
                {
                    rowFilled = true;
                }
                // TODO: add a random startingCol to place
                EnableTileRows(row, startingCol, amountEnabled);
                startingCol += amountEnabled + 1;
            }
        }
    }

    private void EnableTileRows(int rowIndex, int colIndex, int amountEnabled)
    {
        Debug.Log("Current rowIndex is " + rowIndex + ", colIndex is " + colIndex + " and amountEnabled is " + amountEnabled);
        for (int index = 0; index < amountEnabled; index++)
        {
            solutionBoard[rowIndex, colIndex + index].Toggle();
        }
    }
    #endregion 

    private Tile[,] CreateBoard()
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
    #endregion
}