using System.Collections.Generic;

/* Hint
 * Purpose:
 *      To control the hints provided to the player.
 */
public class Hint
{
    #region fields

    private List<int> hintValues;

    #endregion
    
    #region properties

    public int[] HintValues { get => hintValues.ToArray();}

    #endregion

    #region constructors

    public Hint()
    {
        hintValues = new List<int>();
    }

    public Hint(List<int> values)
    {
        hintValues = values;
    }

    #endregion

    #region interface

    public void Add(int value)
    {
        hintValues.Add(value);
    }

    public void Clear()
    {
        hintValues.Clear();
    }

    #endregion
}
