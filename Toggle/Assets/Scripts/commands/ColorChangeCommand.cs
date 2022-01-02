using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeCommand : ICommand
{
    private Button button;
    private Color color;
    private Color previousColor;

    public ColorChangeCommand(Button button, Color color)
    {
        this.button = button;
        this.color = color;
    }

    public void Execute()
    {
        ColorBlock colors = button.colors;
        previousColor = colors.normalColor;
        colors.normalColor = color;
        button.colors = colors;
        Debug.Log("Color changed to " + colors.normalColor.ToString());
    }

    public void Redo()
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = previousColor;
        button.colors = colors;
    }
}
