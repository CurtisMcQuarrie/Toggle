using UnityEngine;
using UnityEngine.UI;

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

    #region ISubscriber implementation
    void ITileObjectSubscriber.Update(TileObject tileObject)
    {
        ChangeColor(tileObject.Tile.IsOn);
    }

    void ISubscriber.Update()
    {
        throw new System.NotImplementedException();
    }
    

    private void ChangeColor(bool isOn)
    {
        button.colors = isOn ? isOnColorBlock : isOffColorBlock;
    }



    #endregion
}
