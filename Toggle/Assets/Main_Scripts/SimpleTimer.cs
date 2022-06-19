using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SimpleTimer : MonoBehaviour
{
    #region fields

    [SerializeField]
    private TextMeshProUGUI textMesh;
    private bool timerActive = false;
    private float currentTime = 0f;


    #endregion

    #region properties

    public bool TimerActive { get => timerActive; set => timerActive = value; }

    #endregion

    #region monobehaviour

    private void Start()
    {
        if (textMesh == null)
        {
            Debug.LogError("The timer has no text GO assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        textMesh.text = " " + time.ToString(@"hh\.mm\.ss");
    }

    #endregion

    #region utility

    public void StartStopWatch()
    {
        timerActive = true;
    }

    public void StopStopWatch()
    {
        timerActive = false;
    }

    public void ResetStopWatch()
    {
        currentTime = 0f;
    }

    #endregion

}
