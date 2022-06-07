using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeControl : MonoBehaviour
{
    #region fields

    [SerializeField]
    private AudioManager audioManager;
    [SerializeField] 
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI text;

    public bool controlsMusic;

    #endregion

    #region monobehaviour

    void Start()
    {
        slider.onValueChanged.AddListener((value) =>
        {
            text.text = value.ToString("0.00");
            if (controlsMusic)
            {
                audioManager.SetMusicVolume(value);
            }
            else
            {
                audioManager.SetFXVolume(value);
            }
        });
    }

    #endregion

}
