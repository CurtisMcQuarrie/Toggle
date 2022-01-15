using UnityEngine;
using UnityEngine.UI;

/* ButtonColorBlockChange
 * Purpose:
 *      Changes the color block of the button gameobject that this script is attached too.
 */ 
[RequireComponent(typeof(Button))]
public class ButtonColorBlockChange : MonoBehaviour, ITileObjectSubscriber
{
    #region fields

    private Button button;

    [Header("Is Off Color Block")]
    public ColorBlock isOffColorBlock;
    [Header("Is On Color Block")]
    public ColorBlock isOnColorBlock;

    #endregion

    #region monobehaviour

    void Start()
    {
        button = GetComponent<Button>();
        ChangeColor(false);
    }

    #endregion

    #region interface

    public void ChangeColor(bool isOn)
    {
        button.colors = isOn ? isOnColorBlock : isOffColorBlock;
    }

    #endregion

    #region interface implementations

    void ITileObjectSubscriber.Update(TileObject tileObject)
    {
        ChangeColor(tileObject.Tile.IsOn);
    }

    void ISubscriber.Update()
    {
        throw new System.NotImplementedException();
    }
    
    #endregion
}
