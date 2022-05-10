using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeCommand : ICommand
{
    private Toggle toggle;
    private Color color;
    private Color previousColor;

    public ColorChangeCommand(Toggle toggle, Color color)
    {
        this.toggle = toggle;
        this.color = color;
    }

    public void Execute()
    {
        ColorBlock colors = toggle.colors;
        previousColor = colors.normalColor;
        colors.normalColor = color;
        toggle.colors = colors;
        Debug.Log("Color changed to " + colors.normalColor.ToString());
    }

    public void Redo()
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        ColorBlock colors = toggle.colors;
        colors.normalColor = previousColor;
        toggle.colors = colors;
    }
}
