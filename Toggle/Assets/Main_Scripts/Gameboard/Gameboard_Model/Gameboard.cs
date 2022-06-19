using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System;

/* Gameboard
 * Purpose:
 *      Holds basic details about the current game.
 *      Handles Tile instantiation, manipulation and destruction for both the solution board and the gameboard.
 *      Computes Hints that are used to solve the game.
 */
public class Gameboard
{
    #region constants

    private const int initialSize = 4; // the starting size if no difficult is specified

    #endregion

    #region fields
    
    private List<List<Tile>> tileList; // the gameboard
    private List<List<Tile>> solutionList; // a single solution to the game
    private List<List<Hint>> hintList; // the hints to the solution
    private int size; // the width and height of the gameboard
    private int minAmountTilesOn;
    private int maxAmountTilesOn;

    #endregion

    #region constructors

    public Gameboard()
    {
        Initialize(); // initialize fields
        SetDifficulty(Difficulty.Easy); // populate fields
    }

    public Gameboard(Difficulty difficulty)
    {
        Initialize(); // initialize fields
        SetDifficulty(difficulty);  // populate fields
    }

    #endregion

    #region properties, getters and setters

    public Difficulty GetDifficulty() { return (Difficulty)(size - initialSize); }
    public int Size { get => size; }

    public int[] GetColumnHints(int column) { return hintList[(int)IndexType.Column][column].HintValues; }
    public int[] GetRowHints(int row) { return hintList[(int)IndexType.Row][row].HintValues; }
    
    public Tile GetTile(int row, int column) { return tileList[row][column]; }

    /* GetTiles (O(n) time complexity, n = size)
     * Purpose:
     *      Retrieves the Tile instances stored in the specified row or column number.
     * Params:
     *      IndexType indexType     Specifies whether to retrieve row or column.
     *      int index               Specifies row or column number to fetch the Tile instances from.
     * Returns: 
     *      Tile[] tiles            All the Tile instances stored in the specified row or column number. 
     */
    public Tile[] GetTiles(IndexType indexType, int index)
    {
        Tile[] tiles = new Tile[size];

        // case A: return a row of tiles
        if (indexType == IndexType.Row)
        {
            tiles = tileList[index].ToArray();
        }
        // case B: return a column of tiles
        else if (indexType == IndexType.Column)
        {
            // loop through each row to get column value
            for (int row = 0; row < size; row++)
            {
                tiles[row] = tileList[row][index];
            }
        }
        
        return tiles;
    }

    #endregion

    #region public methods

    /* ChangeDifficulty
     * Purpose:
     *      Changes the gameboard size and generates a new solution.
     */
    public void ChangeDifficulty(Difficulty difficulty)
    {
        SetDifficulty(difficulty); // change the board size
        GenerateSolution();// generate new solution
        GenerateHints();// generate new hints
    }

    /* CheckSolution
     * Purpose:
     *      Checks if the gameboard state matches the solution.
     */
    public bool CheckSolution()
    {
        return (CheckSolution(IndexType.Row) && CheckSolution(IndexType.Column));
    }

    /* Clear (O(n^2) time complexity, n = size)
     * Purpose:
     *      Sets all Tiles within only the gameboard to false.
     */
    public void Clear()
    {
        // cycle through each Tile inside the tileList
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                tileList[row][col].Reset();
            }
        }
    }

    /* PrintSolution
     * Purpose:
     *      For debug purposes only.
     *      Allows the developer to see a solution in the debug window.
     */
    public void PrintBoard(List<List<Tile>> board)
    {
        StringBuilder solution = new StringBuilder();
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                solution.Append(board[row][col].ToString());
            }
            solution.Append("\n");
        }
        Debug.Log(solution.ToString());
    }

    /* PrintSolution
     * Purpose:
     *      For debug purposes only.
     *      Allows the developer to see a solution in the debug window.
     */
    public void PrintSolution()
    {
        PrintBoard(solutionList);
    }

    /* Reroll
     * Purpose:
     *      Resets the gameboard and generates a new solution.
     */
    public void Reroll()
    {
        Reset(); // reset all boards and hints
        GenerateSolution(); // generate new solution
        GenerateHints(); // generate new hints
    }

    /* Reset (O(n^2) time complexity, n = size)
     * Purpose:
     *      Sets all Tiles within the tileList to false AND empties all hints.
     */
    public void Reset()
    {
        // resets the Tile and Hint collection
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                tileList[row][col].Reset();
                solutionList[row][col].Reset();
                // case A: row index is valid for Hint collection
                if (row < (int)IndexType.Size)
                {
                    hintList[row][col].Clear();
                }
            }
        }
    }

    /* Toggle
     * Purpose:
     *      Changes the state of the Tile to the opposite of its current state.
     * Params:
     *      Tile tile           The Tile instance to call Toggle method.
     */
    public void Toggle(Tile tile)
    {
        tile?.Toggle();
    }

    /* Toggle
     * Purpose:
     *      Changes the state of the Tile to the specified value.
     * Params:
     *      Tile tile           The Tile instance to call Toggle method.
     *      bool isOn           The state to set the Tile instance too.
     */
    public void Toggle(Tile tile, bool isOn)
    {
        tile?.Toggle(isOn);
    }

    /* Toggle
     * Purpose:
     *      Changes the state of the Tile to the opposite of its current state.
     * Params:
     *      int row                 The row to locate the Tile instance.
     *      int column              The column to locate the Tile instance.
     */
    public void Toggle(int row, int column)
    {
        tileList[row][column]?.Toggle();
        if (CheckSolution()) // TODO: move to GameboardController
        {
            Debug.Log("You win!");
        }
    }

    /* Toggle
     * Purpose:
     *      Changes the state of the Tile to the specified value.
     * Params:
     *      int row                 The row to locate the Tile instance.
     *      int column              The column to locate the Tile instance.
     *      bool isOn               The state to set the Tile instance too.
     */
    public void Toggle(int row, int column, bool isOn)
    {
        tileList[row][column]?.Toggle(isOn);
        if (CheckSolution()) // TODO: move to GameboardController
        {
            Debug.Log("You win!");
        }
    }

    #endregion
    
    #region solution generation

    /* GenerateSolution (new and complete)
     * Purpose:
     *      Randomly generates the number tiles in the On state for the solution.
     *      Randomly generates the tuple that indicates which Tiles are on.
     */
    private void GenerateSolution()
    {
        HashSet<Tuple<int, int>> solution = new HashSet<Tuple<int, int>>();
        // calculate the range of tiles that can be on at this difficulty
        minAmountTilesOn = (int) size*size/2;
        maxAmountTilesOn = (int) (minAmountTilesOn * 1.5);
        // generate the number of tiles on in this solution
        System.Random randomIntGen = new System.Random();
        int amountTilesOn = randomIntGen.Next(minAmountTilesOn, maxAmountTilesOn + 1);
        //Debug.Log("Amount = " + amountTilesOn + "\tMin = " + minAmountTilesOn + "\t\tMax = " + maxAmountTilesOn);
        // generate random tuples that represent tile indices inside tileList
        for (int index = 0; index < amountTilesOn; index++)
        {
            // keep generating a random tuple if the generated tuple already exists in the HashSet
            while (!solution.Add(new Tuple<int, int> (randomIntGen.Next(0, size), randomIntGen.Next(0, size)))){}
        }
        // turn on values for solutionList
        foreach (Tuple<int, int> tuple in solution)
        {
            solutionList[tuple.Item1][tuple.Item2].Toggle(true);
        }
    }

    #endregion

    #region hints generation

    /* GenerateHints
     * Purpose:
     *      Computes hints for both rows and columns.
     */
    private void GenerateHints()
    {
        GenerateHints(IndexType.Row);
        GenerateHints(IndexType.Column);
    }

    /* GenerateHints
     * Purpose:
     *      To compute the hints for the specified List in the game.
     * Params:
     *      IndexType indexType     Specifies whether row hints or column hints are being computed.
     */
    private void GenerateHints(IndexType indexType)
    {
        // go through the solutionList and count the number of Tiles that are consecutively On
        for (int index = 0; index < size; index++)
        {
            // heavy lifting is done inside Hint class
            hintList[(int)indexType][index] = new Hint(Hint.ComputeHint(solutionList, index, indexType));
        }
    }

    #endregion

    #region solution checking

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

        // loop through index of row or column
        for (int index = 0; solved && index < size; index++)
        {
            // case A: the current row or column is not correctly solved
            if (!hintList[(int)indexType][index].IsSolved(tileList, index, indexType))
            {
                solved = false;
            }
            // case B: the current row or column is correctly solved
        }
        
        return solved;
    }

    #endregion

    #region initialization

    /* Initialize
     * Purpose:
     *      Initializes all fields within this instance.
     */
    private void Initialize()
    {
        tileList = new List<List<Tile>>();
        solutionList = new List<List<Tile>>();
        hintList = new List<List<Hint>>();

        size = 0;
        minAmountTilesOn = 0;
        maxAmountTilesOn = 0;
    }

    /* HardReset
     * Purpose:
     *      Resets the collections for the tiles and hints references to empty collections.
     *      Also resets the gameboard size to 0.
     */
     private void HardReset()
    {
        Destroy();
        Initialize();
    }

    /* SetDifficulty (O(n^2) time complexity, n = size)
     * Purpose:
     *      Sets the board size.
     * Params:
     *      Difficulty difficulty           Determines the new board size.
     */
    private void SetDifficulty(Difficulty difficulty)
    {
        // case A: a board already exists
        if (size > 0)
        {
            HardReset();
        }
        // case B: this is the first time a board is being created

        size = (int)difficulty + initialSize;

        // initialize new Tile objects inside 2D collection tileList and solutionList
        for (int row = 0; row < size; row++)
        {
            tileList.Add(new List<Tile>());
            solutionList.Add(new List<Tile>());
            for (int col = 0; col < size; col++)
            {
                tileList[row].Add(new Tile());
                solutionList[row].Add(new Tile());
            }
        }

        // add the two lists to store row and column hints
        hintList.Add(new List<Hint>());
        hintList.Add(new List<Hint>());

        // initilaize new Hint objects inside 2D collection hintList
        for (int hint = 0; hint < size; hint++)
        {
            hintList[0].Add(new Hint());
            hintList[1].Add(new Hint());
        }
    }

    #endregion

    #region destruction

    /* Destroy
     * Purpose:
     *      Safely clears the fields and their components.
     */
    public void Destroy()
    {
        tileList.Clear();
        solutionList.Clear();
        hintList.Clear();

        tileList = null;
        solutionList = null;
        hintList = null;
    }

    #endregion

}

public enum Difficulty { Easy, Medium, Hard, Size }

public enum IndexType { Row, Column, Size }