public class Gameboard
{
    #region fields
    //private int[,] solution;
    //private int[,] enabledCounts; // the counts of rows and columns enabled 
    private Tile[,] board;
    private Difficulty currDifficulty;
    #endregion

    #region constructors
    public Gameboard()
    {
        currDifficulty = new Difficulty(Difficulties.EASY);
        createBoard();
    }

    public Gameboard(Difficulty difficulty)
    {
        currDifficulty = difficulty;
        createBoard();
    }
    #endregion

    #region properties

    #endregion

    #region public methods
    public void createBoard()
    {
        board = new Tile[currDifficulty.BoardSize, currDifficulty.BoardSize];
        for (int col = 0; col < currDifficulty.BoardSize; col++)
        {
            for (int row = 0; row < currDifficulty.BoardSize; row++)
            {
                board[row, col] = new Tile();
            }
        }
    }

    public void clearBoard()
    {
        if (board != null)
        {
            for (int col = 0; col < currDifficulty.BoardSize; col++)
            {
                for (int row = 0; row < currDifficulty.BoardSize; row++)
                {
                    board[row, col].Enabled = false;
                }
            }
        }
    }

    public bool checkSolution()
    {
        bool solved = false;
        if (checkRowSolution() && checkColSolution())
        {
            solved = true;
        }
        return solved;
    }

    public void generateSolution()
    {

    }
    #endregion

    #region private methods
    private bool checkColSolution()
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

    private bool checkRowSolution()
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
    #endregion
}