using UnityEngine;

[System.Serializable]
public class LeanTweenTransformer : ILeanTweenExecutable
{
    #region fields

    private GameObject instance;
    [SerializeField] private bool sharedVariables;
    [SerializeField] private float sharedDuration;
    [SerializeField] private float sharedDelay;
    [SerializeField] private LeanTweenType sharedType;
    [SerializeField] private bool sharedIsLooping;
    [SerializeField] private LeanTweenTransform move;
    [SerializeField] private LeanTweenTransform scale;
    [SerializeField] private LeanTweenTransform rotate;

    #endregion

    #region properties

    public LeanTweenTransform Move { get => move; set => move = value; }
    public LeanTweenTransform Scale { get => scale; set => scale = value; }
    public LeanTweenTransform Rotate { get => rotate; set => rotate = value; }
    public GameObject Instance { get => instance; set => instance = value; }
    public bool SharedVariables { get => sharedVariables; set => sharedVariables = value; }

    #endregion

    #region constructor(s)

    public LeanTweenTransformer (GameObject gameObject, Vector3 move, Vector3 scale, Vector3 rotate)
    {
        if (gameObject != null)
        {
            instance = gameObject;
        }
        else
        {
            Debug.LogError("LeanTweenTransform has no gameobject reference.");
        }
        this.move = new LeanTweenTransform(move);
        this.scale = new LeanTweenTransform(scale);
        this.rotate = new LeanTweenTransform(rotate);
    }

    public LeanTweenTransformer(GameObject gameObject, Vector3 move, Vector3 scale, Vector3 rotate, float duration, float delay, LeanTweenType type, bool isLooping)
    {
        if (gameObject != null)
        {
            instance = gameObject;
        }
        else
        {
            Debug.LogError("LeanTweenTransform has no gameobject reference.");
        }
        this.move = new LeanTweenTransform(move);
        this.scale = new LeanTweenTransform(scale);
        this.rotate = new LeanTweenTransform(rotate);
    }

    #endregion

    #region interface method(s)

    public void Execute()
    {
        ExecuteMove();
        ExecuteScale();
        ExecuteRotate();
    }

    #endregion

    #region execute methods

    private void ExecuteMove()
    {
        if (move != null)
        {
            switch (move.IsLooping)
            {
                case true:
                    LeanTween.moveLocal(instance, move.Target, move.Duration).setEase(move.Type).setDelay(move.Delay).setLoopPingPong();
                    break;
                case false:
                    LeanTween.moveLocal(instance, move.Target, move.Duration).setEase(move.Type).setDelay(move.Delay);
                    break;
            }
        }
    }

    private void ExecuteScale()
    {
        if (scale != null)
        {
            switch (scale.IsLooping)
            {
                case true:
                    LeanTween.scale(instance.GetComponent<RectTransform>(), scale.Target, scale.Duration).setEase(scale.Type).setDelay(scale.Delay).setLoopPingPong();
                    break;
                case false:
                    LeanTween.scale(instance.GetComponent<RectTransform>(), scale.Target, scale.Duration).setEase(scale.Type).setDelay(scale.Delay);
                    break;
            }
        }
    }

    private void ExecuteRotate()
    {
        if (rotate != null)
        {
            switch (rotate.IsLooping)
            {
                case true:
                    LeanTween.rotateLocal(instance, rotate.Target, rotate.Duration).setEase(rotate.Type).setDelay(rotate.Delay).setLoopPingPong();
                    break;
                case false:
                    LeanTween.rotateLocal(instance, rotate.Target, rotate.Duration).setEase(rotate.Type).setDelay(rotate.Delay);
                    break;
            }
        }
    }

    #endregion

    #region tween manipulation

    public void SetShared()
    {
        if (SharedVariables)
        {
            move.ChangeValues(sharedDuration, sharedDelay, sharedType, sharedIsLooping);
            scale.ChangeValues(sharedDuration, sharedDelay, sharedType, sharedIsLooping);
            rotate.ChangeValues(sharedDuration, sharedDelay, sharedType, sharedIsLooping);
        }
    }

    public void ResetTransformValues()
    {
        move.ResetValues();
        scale.ResetValues();
        rotate.ResetValues();
    }
    #endregion

}
