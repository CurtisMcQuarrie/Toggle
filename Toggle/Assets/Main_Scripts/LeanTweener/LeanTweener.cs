using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains a collection of LeanTweenTransformer instances and is responsible for manipulating and cycle through it.
/// </summary>
public class LeanTweener : MonoBehaviour
{
    #region fields

    [SerializeField] private List<LeanTweenTransformer> transforms = new List<LeanTweenTransformer>();
    private int index = 0;

    #endregion

    #region monobehaviour

    private void Awake()
    {
        index = 0;
    }

    #endregion

    #region inspector

    /// <summary>
    /// Called whenever the inspector is interacted with.
    /// </summary>
    private void OnValidate()
    {
        // set the LeanTweenTransformer valid
        SetShared();
    }

    private void SetShared()
    {
        foreach (LeanTweenTransformer transformer in transforms)
        {
            transformer.SetShared();
        }
    }

    public void ResetTransformerValues()
    {
        foreach (LeanTweenTransformer transformer in transforms)
        {
            transformer.ResetTransformValues();
        }
    }

    #endregion

    #region tweening

    /// <summary>
    /// Plays the LeanTweens specified in the current LeanTweenTransformer stored in the collection at position "index".
    /// </summary>
    public void Execute()
    {
        // Only execute the next LeanTweenTransformer if the collection is not empty
        if (transforms != null && transforms.Count > 0 && transforms[index] != null)
        {
            LeanTween.cancel(gameObject);
            transforms[index].Execute();
            index = index >= transforms.Count - 1 ? 0 : index + 1;
        }
    }

    /// <summary>
    /// Directly changes the transforms of the GO to the values in the LeanTweenTransformer stored in the collection at position "index".
    /// </summary>
    public void DirectExecute()
    {
        LeanTween.cancel(gameObject);

        // Only go to the next LeanTweenTransformer if the collection is not empty
        if (transforms != null && transforms.Count > 0 && transforms[index] != null)
        {
            RectTransform rect = GetComponent<RectTransform>();

            rect.localPosition = transforms[index].Move.Target;
            rect.localScale = transforms[index].Scale.Target;
            rect.localEulerAngles = transforms[index].Rotate.Target;

            index = index >= transforms.Count - 1 ? 0 : index + 1;
        }
    }

    /// <summary>
    /// Directly changes the transforms of the GO to the values in the first LeanTweenTransformer of the collection.
    /// If there is no first LeanTweenTransformer in the collection then the transforms of the GO are set all to 0.
    /// </summary>
    public void Reset()
    {
        index = 0;
        RectTransform rect = GetComponent<RectTransform>();

        LeanTween.cancel(gameObject);

        // case: there is no element in the collection of LeanTweenTransformers
        if (transforms == null || transforms.Count == 0)
        {
            rect.localPosition = Vector3.zero;
            rect.localScale = Vector3.zero;
            rect.localEulerAngles = Vector3.zero;
        }
        // case: there is an element in the collection of LeanTweenTransformers
        else
        {
            rect.localPosition = transforms[0].Move.Target;
            rect.localScale = transforms[0].Scale.Target;
            rect.localEulerAngles = transforms[0].Rotate.Target;
        }
    }

    #endregion

    #region transform management

    /// <summary>
    /// Adds a new LeanTweenTransformer with values equal to the current transform of the GO.
    /// </summary>
    public void AddTransform()
    {
        RectTransform rect = GetComponent<RectTransform>();
        transforms.Add(new LeanTweenTransformer(gameObject, rect.localPosition, rect.localScale, rect.localEulerAngles));
    }

    /// <summary>
    ///  Adds a new LeanTweenTransformer with values equal to the current transform of the GO with the specified values.
    /// </summary>
    /// <param name="duration">The duration of the LeanTweens.</param>
    /// <param name="delay">The delay of the LeanTweens.</param>
    /// <param name="type">The LeanTweenType of the LeanTweens.</param>
    /// <param name="isLooping">Determines if the LeanTweens loop.</param>
    public void AddTransform(float duration, float delay, LeanTweenType type, bool isLooping)
    {
        RectTransform rect = GetComponent<RectTransform>();
        transforms.Add(new LeanTweenTransformer(gameObject, rect.localPosition, rect.localScale, rect.localEulerAngles, duration, delay, type, isLooping));
    }

    #endregion
}
