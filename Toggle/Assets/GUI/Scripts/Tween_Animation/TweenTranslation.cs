using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenTranslation : MonoBehaviour
{
    #region fields

    [SerializeField] private Vector3 target;
    [SerializeField] private float time;
    [SerializeField] private LeanTweenType type;

    #endregion

    #region monobehaviour

    void Start()
    {
        LeanTween.moveLocal(gameObject, target, time).setEase(type).setLoopPingPong();
    }

    #endregion
}
