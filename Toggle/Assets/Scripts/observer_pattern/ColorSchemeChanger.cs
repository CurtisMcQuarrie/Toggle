using UnityEngine;
using UnityEngine.UI;

public class ColorSchemeChanger : MonoBehaviour, ITileObjectSubscriber
{
    #region fields

    private ColorSchemeHandler colorSchemeHandler;

    [Header("Off Color Scheme")]
    [SerializeField]
    private ColorScheme offColorScheme;
    [Header("On Color Scheme")]
    [SerializeField]
    private ColorScheme onColorScheme;

    #endregion

    #region monobehaviour

    // Start is called before the first frame update
    void Start()
    {
        colorSchemeHandler = GetComponentInChildren<ColorSchemeHandler>();
        ChangeColorScheme(false);
    }

    #endregion

    private void ChangeColorScheme(bool isOn)
    {
        colorSchemeHandler.ActiveColorScheme = isOn ? onColorScheme : offColorScheme;
    }

    #region interface implementations

    void ITileObjectSubscriber.Update(TileObject tileObject)
    {
        ChangeColorScheme(tileObject.Tile.IsOn);
    }

    void ISubscriber.Update()
    {
        throw new System.NotImplementedException();
    }

    #endregion
}