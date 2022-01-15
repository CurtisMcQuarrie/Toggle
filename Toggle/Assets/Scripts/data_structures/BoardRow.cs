using System.Collections.Generic;
using System.Text;

public class BoardRow
{

    #region fields
    private int size;
    private List<Tile> tiles;

    #endregion

    #region properties

    public Tile[] Tiles { get => tiles.ToArray(); }
    public Tile GetTile(int index) { return tiles[index]; }
    public int Size { get => size; }

    #endregion

    #region constructors

    public BoardRow()
    {
        tiles = new List<Tile>();
        size = 0;
    }

    public BoardRow(List<Tile> values)
    {
        tiles = values;
        size = tiles.Count;
    }

    public BoardRow(int rowSize)
    {
        size = rowSize;
        tiles = new List<Tile>();
        for (int i = 0; i < rowSize; i++)
        {
            tiles.Add(new Tile());
        }
    }

    #endregion

    #region interface

    public void Add()
    {
        tiles.Add(new Tile());
        size++;
    }

    public void Add(Tile newTile)
    {
        tiles.Add(newTile);
        size++;
    }

    public void Add(int amountToAdd)
    {
        for (int i = 0; i < amountToAdd; i++)
        {
            tiles.Add(new Tile());
            size++;
        }
    }

    public void Destroy()
    {
        tiles.Clear();
        size = 0;
    }

    public List<int> GetConsecutiveOn()
    {
        List<int> isOnList = new List<int>();
        int consecutiveIsOn = 0;
        for (int index = 0; index < size; index++)
        {
            if (tiles[index].IsOn)
                consecutiveIsOn++;
            if(consecutiveIsOn > 0 && (index == size - 1  || !tiles[index].IsOn))
            {
                isOnList.Add(consecutiveIsOn);
                consecutiveIsOn = 0;
            }
        }
        return isOnList;
    }

    public bool IsEmpty()
    {
        return size == 0;
    }

    public bool IsOn(int index)
    {
        bool isOn = false;
        if (index < tiles.Count && tiles[index].IsOn)
            isOn = true;
        return isOn;
    }

    public void RemoveLast()
    {
        tiles.RemoveAt(tiles.Count - 1);
        size--;
    }

    public void RemoveLast(int amountToRemove)
    {
        if (amountToRemove < tiles.Count)
        {
            for (int i = amountToRemove; i >= 0; i--)
            {
                tiles.RemoveAt(tiles.Count - 1);
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
        foreach (Tile tile in tiles)
        {
            tile.Reset();
        }
    }

    public void ToggleConsecutive(int start, int amount)
    {
        if (amount > 0 && start + amount <= tiles.Count)
        {
            for (int i = start; i < amount; i++)
            {
                tiles[i].Toggle();
            }
        }
        else if (start + amount > tiles.Count || start + amount < 0)
        {
            throw new System.IndexOutOfRangeException("Cannot Toggle amount of Tiles specified in BoardRow.");
        }
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < tiles.Count; i++)
        {
            stringBuilder.Append(tiles[i].ToString());
            if (i != tiles.Count - 1)
                stringBuilder.Append(", ");
        }
        return stringBuilder.ToString();
    }

    #endregion
}
