using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEffect : MonoBehaviour
{

    #region fields

    [SerializeField]
    private string audioClip;

    #endregion

    #region monobehaviour

    private void OnEnable()
    {
        PlayAudio();
        ShowParticles();
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

    private void ShowParticles()
    {
        Debug.LogWarning("Winning particle effects not implemented yet.");
    }

    #endregion
}
