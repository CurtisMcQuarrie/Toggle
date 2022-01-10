/*
 * A single cell in the gameboard table.
 */ 
public class Tile
{
    #region fields
    private bool isOn;
    #endregion

    #region constructors
    public Tile()
    {
        isOn = false;
    }

    public Tile(bool isOn)
    {
        this.isOn = isOn;
    }
    #endregion
    
    #region properties
    public bool IsOn { get => isOn;}
    #endregion

    #region public methods
    public bool Toggle()
    {
        isOn = !isOn;
        return isOn;
    }

    public override string ToString()
    {
        return isOn.ToString();
    }
    #endregion
}
