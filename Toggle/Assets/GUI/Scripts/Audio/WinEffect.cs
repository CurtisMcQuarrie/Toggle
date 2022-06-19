using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEffect : MonoBehaviour
{

    #region fields

    [SerializeField]
    private string audioClip;
    [SerializeField]
    private GameObject titleGO;
    [SerializeField]
    private float titleTweenDuration = 1f;
    [SerializeField]
    private LeanTweenType titleTweenType;
    [SerializeField]
    private Vector3 titleStartPosition;
    [SerializeField]
    private Vector3 titleEndPosition = Vector3.zero;
    [SerializeField]
    private GameObject buttonsGO;
    [SerializeField]
    private float buttonsTweenDuration = 1f;
    [SerializeField]
    private float buttonsTweenDelay = 0f;
    [SerializeField]
    private LeanTweenType buttonsTweenType;
    [SerializeField]
    private Vector3 buttonsStartPosition;
    [SerializeField]
    private Vector3 buttonsEndPosition = Vector3.zero;

    #endregion

    #region monobehaviour

    private void Awake()
    {
        buttonsTweenDelay += titleTweenDuration;
    }

    private void OnEnable()
    {
        PlayAudio();
        PlayGOAnimations();
    }


    private void OnDisable()
    {
        titleGO.GetComponent<RectTransform>().transform.localPosition = titleStartPosition;
        buttonsGO.GetComponent<RectTransform>().transform.localPosition = buttonsStartPosition;
    }


    #endregion

    #region effects

    private void PlayAudio()
    {
        if (audioClip == null)
        {
            Debug.LogWarning("No audio clip for winning.");
        }
        else
        {
            AudioManager.instance.PlaySFX(audioClip);
        }
    }

    private void PlayGOAnimations()
    {
        LeanTween.moveLocal(titleGO, titleEndPosition, titleTweenDuration).setEase(titleTweenType);
        LeanTween.moveLocal(buttonsGO, buttonsEndPosition, buttonsTweenDuration).setEase(buttonsTweenType).setDelay(buttonsTweenDelay);
    }

    #endregion
}
