using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenTranslater : MonoBehaviour
{

    #region fields

    [SerializeField]
    private LeanTweenType tweenType;
    [SerializeField]
    private float tweenDuration;
    [SerializeField]
    private List<Vector3> positions;
    private int currPosition;

    #endregion

    #region properties

    public LeanTweenType TweenType { get => tweenType; set => tweenType = value; }
    public float TweenDuration { get => tweenDuration; set => tweenDuration = value; }

    #endregion

    #region monobehaviour

    void Start()
    {
        currPosition = 0;

        // add the current position if the positions collection is empty
        if (positions.Count < 1)
        {
            positions.Add(gameObject.GetComponent<RectTransform>().localPosition);
        }
    }

    private void OnEnable()
    {
        if (currPosition != 0)
        {
            ResetPosition();
        }
    }

    #endregion

    #region tweening

    public void GoToNextPosition()
    {
        LeanTween.moveLocal(gameObject, GetNextPosition(), tweenDuration).setEase(tweenType);
    }

    public void GoToPreviousPosition()
    {
        LeanTween.moveLocal(gameObject, GetPreviousPosition(), tweenDuration).setEase(tweenType);
    }

    public void ResetPosition()
    {
        gameObject.GetComponent<RectTransform>().localPosition = positions[0];
        currPosition = 0;
    }

    #endregion

    #region position management

    public bool IsAtOrigin()
    {
        return currPosition == 0;
    }

    public Vector3 GetNextPosition()
    {
        currPosition = currPosition >= positions.Count - 1 ? 0 : currPosition + 1;
        return positions[currPosition];
    }

    public Vector3 GetPreviousPosition()
    {
        currPosition = currPosition <= 0 ? positions.Count - 1 : currPosition - 1;
        return positions[currPosition];
    }

    public void AddPosition(Vector3 newPosition)
    {
        positions.Add(newPosition);
    }

    public void AddPosition(int index, Vector3 newPosition)
    {
        positions.Insert(index, newPosition);
    }

    public void RemovePosition(Vector3 positionToRemove)
    {
        positions.Remove(positionToRemove);
    }

    public void RemovePosition(int index)
    {
        positions.RemoveAt(index);
    }

    #endregion
}
