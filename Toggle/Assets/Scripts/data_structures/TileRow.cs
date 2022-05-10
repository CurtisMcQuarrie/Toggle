using System.Collections.Generic;
using System.Text;

public class TileRow
{
    #region fields
    private int size;
    private List<TileObject> tileObjects;

    #endregion

    #region properties

    public TileObject[] TileObjects { get => tileObjects.ToArray(); }
    public TileObject GetTileObject(int index) { return tileObjects[index]; }
    public int Size { get => size; }

    #endregion

    #region constructors

    public TileRow()
    {
        tileObjects = new List<TileObject>();
        size = 0;
    }

    public TileRow(List<TileObject> values)
    {
        tileObjects = values;
        size = tileObjects.Count;
    }

    public TileRow(int rowSize)
    {
        size = rowSize;
        tileObjects = new List<TileObject>();
        for (int i = 0; i < rowSize; i++)
        {
            tileObjects.Add(new TileObject());
        }
    }

    #endregion

    #region interface

    public void Add()
    {
        tileObjects.Add(new TileObject());
        size++;
    }

    public void Add(TileObject newTile)
    {
        tileObjects.Add(newTile);
        size++;
    }

    public void Add(int amountToAdd)
    {
        for (int i = 0; i < amountToAdd; i++)
        {
            tileObjects.Add(new TileObject());
            size++;
        }
    }

    public void Destroy()
    {
        tileObjects.Clear();
        size = 0;
    }

    public bool IsEmpty()
    {
        return size == 0;
    }

    public void RemoveLast()
    {
        tileObjects.RemoveAt(tileObjects.Count - 1);
        size--;
    }

    public void RemoveLast(int amountToRemove)
    {
        if (amountToRemove < tileObjects.Count)
        {
            for (int i = amountToRemove; i >= 0; i--)
            {
                tileObjects.RemoveAt(tileObjects.Count - 1);
                size--;
            }
        }
        else
        {
            throw new System.ArgumentOutOfRangeException("Cannot remove specified amount of Tiles from the BoardRow instance.");
        }
    }

    
    public void Reset()
    {
        foreach (TileObject tileObject in tileObjects)
        {
            tileObject.Reset();
        }
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < tileObjects.Count; i++)
        {
            stringBuilder.Append(tileObjects[i].ToString());
            if (i != tileObjects.Count - 1)
                stringBuilder.Append(", ");
        }
        return stringBuilder.ToString();
    }

    #endregion
}
