public class Tile
{
    #region fields
    private bool enabled;
    //private bool locked;
    //private int damage;
    #endregion

    #region constructors
    public Tile()
    {
        enabled = false;
    }

    public Tile(bool isEnabled)
    {
        Enabled = isEnabled;
    }

    #endregion
    
    #region properties
    public bool Enabled { get => enabled; set => enabled = value; }
    #endregion

    #region methods

    #endregion
}
