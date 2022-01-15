using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* GameboardGUI
 * Purpose:
 *      Instantiates gameboard prefabs for GUI.
 *      Creates only a single object at a time.
 *      Special case for creating a hint because a hint requires children.
 */ 
 // TODO: add customization with scriptable object reference.
public class GameboardGUI : MonoBehaviour
{
    #region fields

    [Header("Hints")]
    public Sprite[] hintSprites;
    public GameObject[] dividerPrefabs = new GameObject[2];
    public GameObject[] hintPanelPrefabs = new GameObject[2];
    public GameObject hintPrefab;
    [Header("Gameboard")]
    public GameObject spacerPrefab;
    public GameObject tilePrefab;

    #endregion

    #region monobehaviour
    #endregion

    #region interface

    #region tiles

    public GameObject CreateTile()
    {
        return CreatePrefab(tilePrefab);
    }

    public GameObject CreateTile(Transform parentTransform)
    {
        return CreatePrefab(tilePrefab, parentTransform);
    }

    public void ClearTile(GameObject tile)
    {
        ButtonColorBlockChange colorBlock = tile.GetComponent<ButtonColorBlockChange>();
        colorBlock?.ChangeColor(false);
    }

    #endregion

    #region hints

    public GameObject CreateHint(int[] hints, IndexType indexType)
    {
        GameObject hintPanel = CreatePrefab(hintPanelPrefabs[(int)indexType]);
        FillHintsPrefab(hints, indexType, hintPanel.GetComponent<Transform>());
        return hintPanel;
    }

    public GameObject CreateHint(int[] hints, IndexType indexType, Transform parentTransform)
    {
        GameObject hintPanel = CreatePrefab(hintPanelPrefabs[(int)indexType], parentTransform);
        FillHintsPrefab(hints, indexType, hintPanel.GetComponent<Transform>());
        return hintPanel;
    }

    public void UpdateHint(int[] newHints, IndexType indexType, GameObject hintObject)
    {
        DestroyChildObjects(transform);
        FillHintsPrefab(newHints, indexType, hintObject.GetComponent<Transform>());
    }

    #endregion

    #region spacer

    public void CreateSpacer()
    {
        CreatePrefab(spacerPrefab);
    }

    public void CreateSpacer(Transform parentTransform)
    {
        CreatePrefab(spacerPrefab, parentTransform);
    }

    #endregion

    #region destroying

    public void DestroyObject(GameObject objectToDestroy)
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

    #endregion

    #region instantiation

    private GameObject CreatePrefab(GameObject prefab)
    {
        GameObject instantiatedObject = null;
        if (prefab != null)
        {
            instantiatedObject = Instantiate(prefab, gameObject.GetComponent<Transform>());
        }
        else
        {
            throw new System.NullReferenceException("Prefab is null when trying instantiate.");
        }
        return instantiatedObject;
    }

    private GameObject CreatePrefab(GameObject prefab, Transform parentTransform)
    {
        GameObject instantiatedObject = null;
        if (prefab != null && parentTransform != null)
        {
            instantiatedObject = Instantiate(prefab, parentTransform);
        }
        else
        {
            throw new System.NullReferenceException("Prefab or parent transform is null when trying instantiate.");
        }
        return instantiatedObject;
    }

    private void FillHintsPrefab(int[] hints, IndexType indexType, Transform parentTransform)
    {
        if (hints != null)
        {
            for (int i = 0; i < hints.Length; i++)
            {
                // add hint to parent panel
                GameObject instantiatedObject = CreatePrefab(hintPrefab, parentTransform);

                // set sprite in hint
                if (hints[i] > 0)
                {
                    Sprite texture = hintSprites[hints[i]-1];
                    instantiatedObject.GetComponent<Image>().sprite = texture;
                }

                // instantiate dividers
                if (i != hints.Length - 1)
                {
                    CreatePrefab(dividerPrefabs[(int)indexType], parentTransform);
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

    private void DestroyChildObjects(Transform parentTransform)
    {
        foreach (Transform child in parentTransform)
        {
            Destroy(child);
        }
    }

    #endregion
}
