using UnityEngine;
using UnityEditor;

/// <summary>
/// A custom editor for the LeanTweener class.
/// </summary>
[CustomEditor(typeof(LeanTweener))]
public class LeanTweenerEditor : Editor
{
    #region fields

    private bool showTransforms = false;
    private float currDuration = 1f;
    private float currDelay = 0f;
    private LeanTweenType currType = LeanTweenType.notUsed;
    private bool currIsLooping = false;

    #endregion

    #region editor methods

    /// <summary>
    /// Displays the custom editor.
    /// </summary>
    public override void OnInspectorGUI()
    {
        LeanTweener leanTweener = (LeanTweener) target;

        TransformAddSection(leanTweener);
        TransformCycleSection(leanTweener);
        InspectorSection(leanTweener);
    }

    #endregion

    #region helper methods

    /// <summary>
    /// Shows the inspector when indicated to do by the showTransformers field.
    /// </summary>
    /// <param name="leanTweener">The respective LeanTween instance this editor is attached too.</param>
    private void InspectorSection(LeanTweener leanTweener)
    {
        GUILayout.Label("Advanced Transformer Details", EditorStyles.boldLabel);

        showTransforms = EditorGUILayout.Toggle("Show Transforms", showTransforms);

        if (showTransforms)
        {
            if (GUILayout.Button("Reset shared values to previous"))
            {
                leanTweener.ResetTransformerValues();
            }

            base.OnInspectorGUI();
        }
    }

    /// <summary>
    /// Adds the editor elements that controls the creation of new LeanTweenTransformers.
    /// </summary>
    /// <param name="leanTweener">The respective LeanTween instance this editor is attached too.</param>
    private void TransformAddSection(LeanTweener leanTweener)
    {
        GUILayout.Label("Add New Transforms", EditorStyles.boldLabel);

        GUILayout.BeginVertical();
        
            currDuration = EditorGUILayout.Slider("Tween Duration", currDuration, 0f, 10f);
            currDelay = EditorGUILayout.Slider("Tween Delay", currDelay, 0f, 10f);
            currType = (LeanTweenType)EditorGUILayout.EnumPopup("Lean Tween Type", currType);
            currIsLooping = EditorGUILayout.Toggle("Looping Is On", currIsLooping);
        
            if (GUILayout.Button("Add Transform"))
            {
                leanTweener.AddTransform(currDuration, currDelay, currType, currIsLooping);
                Debug.Log("Transform has been captured.");
            }

        GUILayout.EndVertical();
    }

    /// <summary>
    /// Adds the editor elements that controls cycling through the collection of LeanTweenTransformers.
    /// </summary>
    /// <param name="leanTweener">The respective LeanTween instance this editor is attached too.</param>
    private void TransformCycleSection(LeanTweener leanTweener)
    {
        GUILayout.Label("Cycle Through Existing Transforms", EditorStyles.boldLabel);
        
        GUILayout.BeginHorizontal();

            if (GUILayout.Button("Next"))
            {
                leanTweener.DirectExecute();
            }
            if (GUILayout.Button("Reset"))
            {
                leanTweener.Reset();
            }
        
        GUILayout.EndHorizontal();
    }

    #endregion
}
