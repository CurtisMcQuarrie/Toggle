public class Difficulty
{
    #region fields
    private int boardSize;
    private Difficulties currDifficulty;
    #endregion

    #region constructors
    public Difficulty()
    {
        setupDifficulty();
    }

    public Difficulty(Difficulties difficulty)
    {
        currDifficulty = difficulty;
        setupDifficulty();
    }
    #endregion

    #region properties
    public int BoardSize { get => boardSize; }

    public Difficulties CurrDifficulty { get => currDifficulty; }
    #endregion

    #region methods
    private void setupDifficulty()
    {
        if (currDifficulty == Difficulties.EASY)
        {
            boardSize = 3;
        } else if (currDifficulty == Difficulties.MEDIUM)
        {
            boardSize = 4;
        } else if (currDifficulty == Difficulties.HARD)
        {
            boardSize = 5;
        }
    }
    #endregion 

}

public enum Difficulties { EASY, MEDIUM, HARD }