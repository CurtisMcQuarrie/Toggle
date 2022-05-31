using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Colors", menuName = "ScriptableObjects/ColorSchemes", order = 1)]
public class ColorScheme : ScriptableObject
{
    public List<Color> colorScheme;
    public Color defaultColor;
}
