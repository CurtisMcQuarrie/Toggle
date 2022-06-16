using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenBouncer : MonoBehaviour
{
    #region fields

    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float widthScale = 1.0f;
    [SerializeField]
    private float heightScale = 1.0f;
    [SerializeField]
    private LeanTweenType type = LeanTweenType.easeInOutBounce;

    private RectTransform rect;
    private Vector3 originalScale;
    private Vector3 targetScale;

    #endregion

    #region monobehaviour

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        originalScale = rect.localScale;
        targetScale.x = originalScale.x * widthScale;
        targetScale.y = originalScale.y * heightScale;
        targetScale.z = originalScale.z;
        Bounce();
    }

    #endregion

    #region methods

    private void Bounce()
    {
        LeanTween.scale(rect, targetScale, speed).setEase(type).setLoopPingPong();
    }

    #endregion

}
