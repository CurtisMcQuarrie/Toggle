using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TweenScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    #region fields
    [SerializeField]
    private bool scaleOnHover = false;
    [SerializeField]
    private bool scaleOnClick = false;


    private RectTransform rect;

    [SerializeField]
    private float widthScale = 1.0f;
    [SerializeField]
    private float heightScale = 1.0f;
    [SerializeField]
    private float inDuration = 1.0f;
    [SerializeField]
    private LeanTweenType inType = LeanTweenType.clamp;
    [SerializeField]
    private LeanTweenType outType = LeanTweenType.clamp;
    [SerializeField]
    private float outDuration = 1.0f;

    private Vector3 originalScale;
    private Vector3 targetScale;

    #endregion

    #region monobehaviour

    void Awake()
    {
        rect = gameObject.GetComponent<RectTransform>();
    }

    void Start()
    {
        originalScale = rect.localScale;
        targetScale.x = originalScale.x * widthScale;
        targetScale.y = originalScale.y * heightScale;
        targetScale.z = originalScale.z;
    }

    #endregion

    #region pointerHandler

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (scaleOnHover)
        {
            Scale(targetScale, inDuration, inType);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (scaleOnHover)
        {
            Scale(originalScale, outDuration, outType);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (scaleOnHover)
        {
            Scale(originalScale, outDuration, outType);
        }
        else if (scaleOnClick)
        {
            Scale(targetScale, inDuration, inType);
        }
    }

    #endregion

    private void Scale(Vector3 newScale, float duration, LeanTweenType type)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(rect, newScale, duration).setEase(type);
    }

    private void ResetScale()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(rect, originalScale, outDuration).setEase(outType);
    }
}
