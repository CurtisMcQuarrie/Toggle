using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LeanTweenTransform
{
    #region fields

    [SerializeField] private Vector3 target = Vector3.one;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float delay = 0f;
    [SerializeField] private LeanTweenType type = LeanTweenType.notUsed;
    [SerializeField] private bool isLooping = false;

    // second set of variables for when the LeanTweenerEditor set shared variables on
    private float archivedDuration = 1f;
    private float archivedDelay = 0f;
    private LeanTweenType archivedType = LeanTweenType.notUsed;
    private bool archivedIsLooping = false;

    #endregion

    #region properties

    public Vector3 Target { get => target; set => target = value; }
    public float Duration { get => duration; set => duration = value; }
    public float Delay { get => delay; set => delay = value; }
    public LeanTweenType Type { get => type; set => type = value; }
    public bool IsLooping { get => isLooping; set => isLooping = value; }

    #endregion

    #region constructor(s)

    public LeanTweenTransform(Vector3 target)
    {
        this.target = target;
    }

    public LeanTweenTransform(Vector3 target, float duration, float delay, LeanTweenType type, bool isLooping) : this(target)
    {
        ChangeValues(duration, delay, type, isLooping);
    }

    #endregion

    #region variable manipulation

    public void ChangeValues(float duration, float delay, LeanTweenType type, bool isLooping)
    {
        StoreArchivedValues();
        this.duration = duration;
        this.delay = delay;
        this.type = type;
        this.isLooping = isLooping;
    }

    public void ResetValues()
    {
        duration = archivedDuration;
        delay = archivedDelay;
        type = archivedType;
        isLooping = archivedIsLooping;
    }

    private void StoreArchivedValues()
    {
        archivedDuration = duration;
        archivedDelay = delay;
        archivedType = type;
        archivedIsLooping = isLooping;
    }

    #endregion
}
