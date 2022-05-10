using System.Collections.Generic;
using System.Text;

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

    public void Destroy()
    {
        hintValues.Clear();
    }

    public bool IsEqual(List<int> other)
    {
        bool isEqual = true;
        if (other.Count == hintValues.Count)
        {
            for (int i = 0; isEqual && i < hintValues.Count; i++)
            {
                if (other[i] != hintValues[i])
                    isEqual = false;
            }
        }
        else
            isEqual = false;
        return isEqual;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < hintValues.Count; i++)
        {
            stringBuilder.Append(hintValues[i]);
            if (i < hintValues.Count - 1)
                stringBuilder.Append(", ");
        }
        return stringBuilder.ToString();
    }

    #endregion
}
