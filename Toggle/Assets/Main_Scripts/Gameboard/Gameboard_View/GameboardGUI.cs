using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

/* GameboardGUI
 * Purpose:
 *      Instantiates gameboard prefabs for GUI.
 *      Creates only a single object at a time.
 *      Special case for creating a hint because a hint requires children.
 */
public class GameboardGUI : MonoBehaviour
{
    #region fields

    [Header("Hints")]
    public GameObject hintPrefab;
    [Header("Gameboard")]
    public GameObject spacerPrefab;
    public GameObject tilePrefab;
    [Header("Panels")]
    public Transform gameboardPanel;
    public GameObject gamePanel;
    public GameObject winPanel;

    private List<GameObject> gameObjectList;

    #endregion

    #region monobehaviour

    private void Awake()
    {
        gameObjectList = new List<GameObject>();
    }

    #endregion

    #region public methods

    #region gameboard

    public List<List<TileObject>> CreateBoard(Gameboard gameboard)
    {
        List<List<TileObject>> tileObjects = new List<List<TileObject>>();

        CreatePrefab(spacerPrefab); // instantiate spacer
        
        // instantiate ColumnHints
        for (int col = 0; col < gameboard.Size; col++)
        {
            CreateHint(gameboard.GetColumnHints(col), IndexType.Column);
        }
        
        // instantiate RowHints and Tiles
        for(int row = 0; row < gameboard.Size; row++)
        {
            CreateHint(gameboard.GetRowHints(row), IndexType.Row);

            tileObjects.Add(new List<TileObject>());
            for(int col = 0; col < gameboard.Size; col++)
            {
                GameObject tile = CreateTile();
                TileObject tileObject = tile.GetComponent<TileObject>();
                tileObjects[row].Add(tileObject);
            }
        }

        return tileObjects;
    }

    #endregion

    #region tiles

    private GameObject CreateTile()
    {
        return CreatePrefab(tilePrefab);
    }

    #endregion

    #region hints

    private void CreateHint(int[] hints, IndexType indexType)
    {
        FillHintsPrefab(hints, indexType, gameboardPanel);
    }

    #endregion

    #region destroying

    public void DestroyAll()
    {
        foreach(GameObject gameObject in gameObjectList)
        {
            DestroyChildObjects(gameObject.transform);
            Destroy(gameObject);
        }
        gameObjectList.Clear();
    }

    private void DestroyObject(GameObject objectToDestroy)
    {
        if (objectToDestroy != null)
        {
            DestroyChildObjects(objectToDestroy.transform); // only runs for hint prefab gameobjects
            Destroy(objectToDestroy);
        }
        else
        {
            throw new System.NullReferenceException("Trying to destroy an object that does not exist.");
        }
    }

    #endregion

    #region panels

    public void DisplayWinPanel(bool showPanel)
    {
        winPanel.SetActive(showPanel);
    }

    public void DisplayGamePanel(bool showPanel)
    {
        gamePanel.SetActive(showPanel);
    }

    #endregion

    #endregion

    #region instantiation

    /* CreatePrefab
     * Purpose:
     *      Creates the specified prefab as a child object of this script's gameobject.
     * Params:
     *      GameObject prefab               The prefab to instantiate.
     */
    private GameObject CreatePrefab(GameObject prefab)
    {
        GameObject instantiatedObject = null;

        if (prefab != null)
        {
            instantiatedObject = Instantiate(prefab, gameboardPanel);
            gameObjectList.Add(instantiatedObject);
        }
        else
        {
            throw new System.NullReferenceException("Prefab is null when trying instantiate.");
        }

        return instantiatedObject;
    }

    /* CreatePrefab
     * Purpose:
     *      Creates the specified prefab as a child objet to the specified parent transform.
     * Params:
     *      GameObject prefab               The prefab to instantiate.
     *      Transform parentTransform       The transform that this prefab will be instantiated under.
     */
    private GameObject CreatePrefab(GameObject prefab, Transform parentTransform)
    {
        GameObject instantiatedObject = null;

        if (prefab != null && parentTransform != null)
        {
            instantiatedObject = Instantiate(prefab, parentTransform);
            gameObjectList.Add(instantiatedObject);
        }
        else
        {
            throw new System.NullReferenceException("Prefab or parent transform is null when trying instantiate.");
        }

        return instantiatedObject;
    }

    /* FillHintsPrefab
     * Purpose:
     *      Fills in the hints with actual integer values.
     * Params:
     *      int[] hints                     The integer values to populate the hints prefab with.
     *      IndexType indexType             Identifies the type of divider prefab to use.
     *      Transform parentTransform       The transform to attach the instantiated hints onto.
     */
    private void FillHintsPrefab(int[] hints, IndexType indexType, Transform parentTransform)
    {
        TextMeshProUGUI hintText = null;
        if (hints != null)
        {
            // add hint to parent panel
            hintText = CreatePrefab(hintPrefab.gameObject, parentTransform).GetComponentInChildren<TextMeshProUGUI>();
            hintText.text = "";

            for (int i = 0; i < hints.Length; i++)
            {
                // set hint text
                hintText.text += hints[i];
                
                // add hyphen if not the last hint
                if ((i + 1) % 2 == 0)
                {
                    hintText.text += "\n";
                }
                else if (i < hints.Length - 1)
                {
                    hintText.text += "-";
                }
            }
        }
        else
        {
            throw new System.NullReferenceException("Trying to create Hints gameobjects with null hints");
        }
    }

    #endregion

    #region destruction

    /* DestroyChildObjects
     * Purpose:
     *      Removes all child objects attached to the specified gameobject.
     * Params:
     *      Transform parentTransform       The transform of the parent gameobject that will have 
     *                                      all of its children deleted.
     */
    private void DestroyChildObjects(Transform parentTransform)
    {
        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }
    }

    #endregion

}
