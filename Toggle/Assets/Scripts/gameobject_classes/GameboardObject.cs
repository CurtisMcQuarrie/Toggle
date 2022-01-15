using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// instantiates tile and spacer prefabs
public class GameboardObject : MonoBehaviour
{
    #region fields
    public GameObject tilePrefab;
    public GameObject spacerPrefab;
    #endregion

    #region monobehaviour
    #endregion

    #region public methods
    // instantiates a row of tile prefabs
    public GameObject[] CreateBoardRow(Tile[] rowTiles, Transform parentTransform)
    {
        GameObject[] rowTileObjects = new GameObject[rowTiles.Length];
        if (rowTiles == null)
        {
            Debug.Log("Missing Tile array when instantiating row of Tile GameObjects.");
        }
        else if (parentTransform == null)
        {
            Debug.Log("Missing parentTransform when instantiating row of Tile GameObjects.");
        }
        else
        {
            for (int col = 0; col < rowTileObjects.Length; col++)
            {
                rowTileObjects[col] = Instantiate(tilePrefab, parentTransform);
            }
        }
        return rowTileObjects;
    }

    // instanties space prefab
    public void CreateSpacerPanel(Transform parentTransform)
    {
        if(spacerPrefab != null)
        {
            Instantiate(spacerPrefab, parentTransform);
        }
        else
        {
            Debug.Log("Spacer prefab is null when trying to instantiate.");
        }
    }
    #endregion

}
