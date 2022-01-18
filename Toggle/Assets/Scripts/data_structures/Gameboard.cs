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
    
    private List<BoardRow> gameboardList;
    private List<BoardRow> solutionBoardList;
    private List<Hint> rowHints;
    private List<Hint> columnHints;
    private int size;
    private bool gameWon;

    #endregion

    #region constructors

    public Gameboard()
    {
        Initialize(); // initialize fields
        SetDifficulty(Difficulty.Easy); // set difficulty
        Setup(); // setup boards (game and solution)
    }

    public Gameboard(Difficulty difficulty)
    {
        Initialize(); // initialize fields
        SetDifficulty(difficulty); // set difficulty
        Setup(); // setup boards (game and solution)
    }

    #endregion

    #region properties, getters and setters

    public Difficulty GetDifficulty() { return (Difficulty)(size - initialSize); }
    public int Size { get => size; }

    public int[] GetColumnHints(int column) { return columnHints[column].HintValues; }
    public int[] GetRowHints(int row) { return rowHints[row].HintValues; }
    
    public Tile GetTile(int row, int column) { return gameboardList[row].GetTile(column); }
    /* GetTiles (modified and completed)
     * Purpose:
     *      Retrieves the Tile instances stored in the specified row or column number.
     * Params:
     *      IndexType indexType     Specifies whether to retrieve row or column.
     *      int index               Specifies row or column number to fetch the Tile instances from.
     * Returns: 
     *      Tile[] rowTiles         All the the Tile instances stored in the specified column number. 
     */
    public Tile[] GetTiles(IndexType indexType, int index)
    {
        Tile[] tiles = new Tile[size];

        if (indexType == IndexType.Row)
        {
            tiles = gameboardList[index].Tiles;
        }
        else if (indexType == IndexType.Column)
        {
            for (int row = 0; row < size; row++)
            {
                tiles[row] = gameboardList[row].GetTile(index);
            }
        }
        
        return tiles;
    }
    #endregion
    
    #region interface
    
    public void Toggle(Tile tile)
    {
        tile?.Toggle();
        // everytime a Tile is toggled, you want to check the win condition
        // TODO: move solution checking to GameboardController
        if (CheckSolution())
            Debug.Log("You Win!");
    }

    public void Toggle(Tile tile, bool isOn)
    {
        tile?.Toggle(isOn);
        // everytime a Tile is toggled, you want to check the win condition
        // TODO: move solution checking to GameboardController
        if (CheckSolution())
            Debug.Log("You Win!");
    }

    /* Reset (new and complete)
     * Purpose:
     *      Sets all Tiles within the gameboard and solution board to false.
     */
    public void Reset()
    {
        gameWon = false;
        for (int index = 0; index < size; index++)
        {
            gameboardList[index].Reset();
            solutionBoardList[index].Reset();
            rowHints[index].Destroy();
            columnHints[index].Destroy();
        }
    }

    /* Clear (new and complete)
     * Purpose:
     *      Sets all Tiles within only the gameboard to false.
     */ 
    public void Clear()
    {
        for (int row = 0; row < size; row++)
        {
            gameboardList[row].Reset();
        }
    }

    /* CheckSolution (complete)
     * Purpose:
     *      Checks if the gameboard state matches the solution.
     */
    public bool CheckSolution()
    {
        gameWon = (CheckSolution(IndexType.Row) && CheckSolution(IndexType.Column));
        return gameWon;
    }

    /* PrintSolution (new and completed)
     * Purpose:
     *      For debug purposes only.
     *      Allows the developer to see the current solution in the debug window.
     */
    public void PrintSolution()
    {
        StringBuilder solution = new StringBuilder();
        for (int row = 0; row < size; row++)
        {
            solution.Append(solutionBoardList[row].ToString());
            solution.Append("\n");
        }
        Debug.Log(solution.ToString());
    }

    /* ChangeDifficulty (new)
     * Purpose:
     *      Changes the gameboard size and generates a new solution without creating new objects unless necessary.
     */ 
    public void ChangeDifficulty(Difficulty difficulty)
    {
        SetDifficulty(difficulty); // adjust board size
        Reset(); // reset all boards and hints
        GenerateSolution();// generate new solution
        GenerateHints();// generate new hints
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

    #endregion
    
    #region solution generation

    /* GenerateSolution (new and complete)
     * Purpose:
     *      Randomly generates the number of rows enabled in the solutionBoard.
     *      Randomizes by rows.
     */
    private void GenerateSolution()
    {
        for (int row = 0; row < size; row++)
        {
            bool filled = false;
            // have random starting column for first entry
            int startingCol = Random.Range(0, size-1); //changed from 0
            int maxEnabled = size + 1 - startingCol;
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
                // TODO: balance the challenge (getting too many zeros or too many large numbers)
                solutionBoardList[row].ToggleConsecutive(startingCol, amountEnabled);
                startingCol += amountEnabled + Random.Range(1,2);//1; TODO: change "2" into valid num
            }
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

    /* GenerateHints (modified and needs cleanup)
     * Purpose:
     *      To compute the hints for the specified List in the game.
     * Params:
     *      IndexType indexType     Specifies whether row hints or column hints are being computed.
     */
    // TODO: split into 2 methods for clarity (are no longer duplicate code)
    private void GenerateHints(IndexType indexType)
    {
        if (indexType == IndexType.Row) // generate row hints
        {
            for (int row = 0; row < size; row++)
            {
                List<int> consec = solutionBoardList[row].GetConsecutiveOn();
                rowHints[row] = new Hint(consec);
            }
        }
        else if (indexType == IndexType.Column) // generate column hints
        {
            for (int col = 0; col < size; col++)
            {
                List<int> isOnList = new List<int>();
                int consecutiveIsOn = 0;
                for (int row = 0; row < size; row++)
                {
                    if (solutionBoardList[row].IsOn(col))
                        consecutiveIsOn++;
                    if (consecutiveIsOn > 0 && (row == size - 1 || !solutionBoardList[row].IsOn(col)))
                    {
                        isOnList.Add(consecutiveIsOn);
                        consecutiveIsOn = 0;
                    }
                }
                columnHints[col] = new Hint(isOnList);
            }
        }
    }

    #endregion

    #region solution checking
    
    /* CheckSolution (modified and complete)
     * Purpose:
     *      Checks whether or not all columns or rows match the hints.
     * Params:
     *      IndexType indexType     Determines whether the row or column hints are being checked.
     * Returns:
     *      bool solved             True if all Tile isOn fields match the hints.
     */ 
     // TODO: split into 2 methods for clarity (are no longer duplicate code)
    private bool CheckSolution(IndexType indexType)
    {
        bool solved = true;
        if (indexType == IndexType.Row)
        {
            for (int row = 0; solved && row < size; row++)
            {
                if (!rowHints[row].IsEqual(gameboardList[row].GetConsecutiveOn()))
                {
                    solved = false;
                }
            }
        }
        else if (indexType == IndexType.Column)
        {
            for(int col = 0; solved && col < size; col++) // go through each column and compare built boardState
            {
                List<int> columnState = new List<int>();
                int consecutiveIsOn = 0;
                for (int row = 0; row < size; row++)
                {
                    if (gameboardList[row].IsOn(col))
                    {
                        consecutiveIsOn++;
                    }
                    if (consecutiveIsOn > 0 && (row == size - 1 || !solutionBoardList[row].IsOn(col)))
                    {
                        columnState.Add(consecutiveIsOn);
                        consecutiveIsOn = 0;
                    }
                }
                if (!columnHints[col].IsEqual(columnState))
                    solved = false;
            }
        }
        return solved;
    }

    #endregion

    #region initialization

    /* Initialize (new and completed)
     * Purpose:
     *      Initializes all fields within this instance.
     *      All fields will be initialized after calling this.
     *      Only needs to be called by constructor.
     */
    private void Initialize()
    {
        gameWon = false;
        rowHints = new List<Hint>();
        columnHints = new List<Hint>();
        gameboardList = new List<BoardRow>();
        solutionBoardList = new List<BoardRow>();
        size = 0;
    }

    /* Setup (new and completed)
     * Purpose:
     *      Populates each collection with appropriate object type.
     *      All collections will be filled after calling this.
     *      Only needs to be called by constructor.
     */
    private void Setup()
    {
        for (int index = 0; index < size; index++)
        {
            gameboardList.Add(new BoardRow(size));
            solutionBoardList.Add(new BoardRow(size));
            rowHints.Add(new Hint());
            columnHints.Add(new Hint());
        }
    }

    /* SetDifficulty (new and completed)
     * Purpose:
     *      Sets the board size.
     *      Adjusts the current boardsize if needed.
     *      Does NOT clear old contents.
     * Params:
     *      Difficulty difficulty           The difficulty to set the board sizes too.
     */
    private void SetDifficulty(Difficulty difficulty)
    {
        int oldSize = size;
        size = (int)difficulty + initialSize;
        if (oldSize > 0) // only perform if board has already been initialized
        {
            int amountToChange = size - oldSize;
            if (oldSize < size) // board size needs to be increased
            {
                for (int row = 0; row < oldSize; row++) // increase column size
                {
                    gameboardList[row].Add(amountToChange);
                    solutionBoardList[row].Add(amountToChange);
                }
                for (int i = 0; i < amountToChange; i++) // increase row size
                {
                    gameboardList.Add(new BoardRow(size)); // add rows to gameboard
                    solutionBoardList.Add(new BoardRow(size)); // add rows to solutionBoard
                    rowHints.Add(new Hint()); // add columns to rowHints
                    columnHints.Add(new Hint()); // add rows to columnHints
                }
            }
            else if (oldSize > 0 && size < oldSize) // board size needs to be decreased
            {
                amountToChange = -amountToChange;
                for (int i = 0; i < amountToChange; i++) // decrease row size
                {
                    gameboardList.RemoveAt(gameboardList.Count - 1);
                    solutionBoardList.RemoveAt(solutionBoardList.Count - 1);
                    rowHints.RemoveAt(rowHints.Count - 1);
                    columnHints.RemoveAt(rowHints.Count - 1);
                }
                for (int row = 0; row < size; row++) // decrease column size
                {
                    gameboardList[row].RemoveLast(amountToChange);
                    solutionBoardList[row].RemoveLast(amountToChange);
                }
            }
        }
        // boards being initialized does not need to have it's boardsize adjusted
    }
    #endregion

    #region destruction

    /* Destroy (complete)
     * Purpose:
     *      To safely clear the fields and their components.
     */
    public void Destroy()
    {
        for (int index = 0; index < size; index++)
        {
            rowHints[index].Destroy();
            columnHints[index].Destroy();
            gameboardList[index].Destroy();
            solutionBoardList[index].Destroy();
        }
        rowHints = null;
        columnHints = null;
        gameboardList = null;
        solutionBoardList = null;
    }

    #endregion

}

public enum Difficulty { Easy, Medium, Hard }

public enum IndexType { Row, Column }