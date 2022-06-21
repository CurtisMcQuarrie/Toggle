using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetSliderPosition : MonoBehaviour
{
    #region fields

    private Scrollbar scrollbar;

    #endregion

    #region monobehaviour

    void Awake()
    {
        scrollbar = GetComponent<Scrollbar>();
    }

    private void OnEnable()
    {
        scrollbar.value = 1;
    }

    #endregion
}
