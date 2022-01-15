using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// instantiates hints prefab with correct sprites
public class HintsObject : MonoBehaviour
{
    #region fields
    public Sprite[] hint_sprites = new Sprite[6];
    public GameObject[] divider_prefabs = new GameObject[2];
    public GameObject[] hint_panel_prefabs = new GameObject[2];
    public GameObject hint_prefab;
    #endregion

    #region monobehaviour
    #endregion

    #region methods
    /*
     * instantiates hints prefab
     */ 
    public GameObject CreateHint(int[] hints, HintsPrefabs hintPrefab, Transform parentTransform)
    {
        GameObject hintPanel = Instantiate(hint_panel_prefabs[(int) hintPrefab], parentTransform);
        Transform hintPanelTransform = hintPanel.GetComponent<Transform>();

        if (hints != null)
        {
            for (int i = 0; i < hints.Length; i++)
            {
                // instantiate hint gameobject with correct image
                Sprite hintTexture = hint_sprites[hints[i] - 1];
                //GameObject hintObject = new GameObject("hint_image");
                //Instantiate(hintObject, parentTransform);
                GameObject hintObject = Instantiate(hint_prefab, hintPanelTransform);
                //Instantiate(, hintPanelTransform);
                //Image hintImage = hintObject.AddComponent<Image>();
                hintObject.GetComponent<Image>().sprite = hintTexture;

                // instantiate dividers
                if (i != hints.Length - 1)
                {
                    Instantiate(divider_prefabs[(int) hintPrefab], hintPanelTransform);
                }
            }
        }
        return hintPanel;
    }
    #endregion
}

#region enums
public enum HintsPrefabs { ROW, COL };
#endregion