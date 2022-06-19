using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonAudioPlayer : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    #region fields

    [SerializeField]
    private string buttonEnterClip;
    [SerializeField]
    private string buttonClickClip;

    #endregion

    #region interface implementations

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonEnterClip == null)
        {
            Debug.LogWarning("No audio clip for button enter clip.");
        }
        else
        {
            AudioManager.instance.PlaySFX(buttonEnterClip);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (buttonClickClip == null)
        {
            Debug.LogWarning("No audio clip for button click clip.");
        }
        else
        {
            AudioManager.instance.PlaySFX(buttonClickClip);
        }
    }

    #endregion
}
