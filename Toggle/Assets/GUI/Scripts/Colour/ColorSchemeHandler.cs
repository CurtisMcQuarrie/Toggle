using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used in the GlassButton prefab.
/// Changes the colors for each child object to match the supplied ColorScheme ScriptableObject.
/// DOES NOT handle when child GOs are removed or added.
/// </summary>
[ExecuteAlways]
public class ColorSchemeHandler : MonoBehaviour
{
    #region fields
    [SerializeField]
    private GameObject parentObject;
    [SerializeField]
    private ColorScheme activeColorScheme;
    private List<Image> childImages;

    #endregion

    #region propreties

    /// <summary>
    /// Retreives active color scheme
    /// Sets active color scheme.
    /// When setting, updates the colors.
    /// </summary>
    public ColorScheme ActiveColorScheme {
        get => activeColorScheme;
        set
        {
            activeColorScheme = value;
            Refresh();
            SetColors();
        }
    }

    #endregion

    #region monobehaviour

    /// <summary>
    /// Retrieves the children GOs under this GO.
    /// </summary>
    void Start()
    {
        Refresh();
    }

    /// <summary>
    /// Called whenever a field is updated in the inspector.
    /// </summary>
    private void OnValidate()
    {
        Refresh();

        if (childImages != null)
        {
            SetColors();
        }
        else
        {
            ResetColors();
            Debug.Log("Either the color scheme or child images collections are null.");
        }
    }

    #endregion

    #region handle color

    /// <summary>
    /// Uses the provided ColorSchemeScriptableObject to set the child objects colors.
    /// </summary>
    public void SetColors()
    {
        int numColors = (activeColorScheme != null) ? activeColorScheme.colorScheme.Count : 0;
        // loop through each child object under this GO
        for (int index = 0; index < childImages.Count; index++)
        {
            // case: the colorScheme does not exist
            if (ActiveColorScheme == null)
            {
                childImages[index].color = Color.white; // set the default value to white
            }
            // case: cannot use the value specified in the colorScheme
            else if (index >= numColors)
            {
                childImages[index].color = activeColorScheme.defaultColor;
            }
            // case: sets the color to the value specified in the colorScheme
            else
            {
                childImages[index].color = activeColorScheme.colorScheme[index];
            }
        }
    }

    /// <summary>
    /// Changes the colors for each child to white.
    /// </summary>
    private void ResetColors()
    {
        // loop through each child under this GO
        foreach (Image image in childImages)
        {
            image.color = Color.white;
        }
    }

    private void Refresh()
    {
        if (parentObject == null)
        {
            parentObject = gameObject;
        }

        childImages = new List<Image>(parentObject.GetComponentsInChildren<Image>());
    }

    #endregion
}