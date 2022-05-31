/* Tile
 * Purpose:
 *      Control its own state within the gameboard.
 *      Basic game piece that the user manipulates.
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

    #region isOn manipulation

    public bool Toggle()
    {
        isOn = !isOn;
        return isOn;
    }

    public bool Toggle(bool isOn)
    {
        this.isOn = isOn;
        return this.isOn;
    }

    public bool Reset()
    {
        isOn = false;
        return isOn;
    }

    #endregion

    #region utility

    public override string ToString()
    {
        return isOn.ToString();
    }

    #endregion
}
