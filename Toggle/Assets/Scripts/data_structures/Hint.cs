using System.Collections.Generic;
using System.Text;
using UnityEngine;

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

    #region static methods

    /* ComputeHint
     * Purpose:
     *      To compute the hints for the specified List in the game.
     * Params:
     *      List<List<Tile>> solution   The solution board to determine the hint values.
     *      int setIndex                What row or column is being considered.
     *      IndexType indexType         Specifies whether row hints or column hints are being computed.
     */
    public static List<int> ComputeHint(List<List<Tile>> solution, int setIndex, IndexType indexType)
    {
        int consecutiveOn = 0;
        List<int> computedHints = new List<int>();
        // loop through the indices of the solution and count the Tiles that are consecutively on
        for (int index = 0; index < solution.Count; index++)
        {
            Tile currTile = (indexType == IndexType.Row) ? solution[setIndex][index] : solution[index][setIndex];

            // case A: the tile is not on and there is a value to add to the hint
            if (!currTile.IsOn && consecutiveOn > 0)
            {
                computedHints.Add(consecutiveOn);
                consecutiveOn = 0;
            }
            // case B: the tile is last tile in the row and it is on
            else if ((index == solution.Count - 1) && currTile.IsOn)
            {
                consecutiveOn++;
                computedHints.Add(consecutiveOn);
                consecutiveOn = 0;
            }
            // case C: the tile is on
            else if (currTile.IsOn)
            {
                consecutiveOn++;
            }
        }

        return computedHints;
    }

    #endregion

    #region public methods

    public bool IsSolved(List<List<Tile>> solution, int setIndex, IndexType indexType)
    {
        return (IsEqual(Hint.ComputeHint(solution, setIndex, indexType))); 
    }

    public void Clear()
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
