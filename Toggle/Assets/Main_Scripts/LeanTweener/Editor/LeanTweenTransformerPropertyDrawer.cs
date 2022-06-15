using UnityEngine;
using UnityEditor;

//[CustomPropertyDrawer(typeof(LeanTweenTransformer))]
public class LeanTweenTransformerPropertyDrawer: PropertyDrawer
{
    #region fields

    private bool sharedVariables = false;

    #endregion

    #region editor methods

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //Debug.Log(label.text);
        base.OnGUI(position, property, label);
    }

    //public override void OnGUI(Rect po)
    //{
    //    LeanTweenTransformer transformer = (LeanTweenTransformer)target;

    //    sharedVariables = EditorGUILayout.Toggle("Shared Variables", sharedVariables);

    //    if (sharedVariables)
    //    {
    //        // show shared variables

    //    }

    //    base.OnInspectorGUI();
    //}

    #endregion

    #region helper methods

    #endregion
}
