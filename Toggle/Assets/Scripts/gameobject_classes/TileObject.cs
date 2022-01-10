using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileObject : MonoBehaviour
{
    #region fields
    private Gameboard gameboard;
    private Toggle toggle;
    private Tile tile;
    public List<ICommand> commands;
    public Color enabledColor;
    public Color disabledColor;
    #endregion

    #region properties
    public Tile Tile { set => tile = value; }
    public Gameboard Gameboard { set => gameboard = value; }
    #endregion

    #region monobehaviour
    void Awake()
    {
        commands = new List<ICommand>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        toggle = this.GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChange);
    }
    #endregion

    #region methods
    public void SetupCommands()
    {
        if (gameboard != null && tile != null)
        {
            commands.Add(new ToggleCommand(gameboard, tile));
        }
        else
        {
            Debug.Log("Either gameboard or tile fields are null for this tile.");
        }
    }

    private void ExecuteCommands()
    { 
        ToggleCommand toggleCommand = new ToggleCommand(gameboard, tile);
        CommandManager.Instance.AddCommand(toggleCommand);
        toggleCommand.Execute();
    }

    private void OnToggleValueChange(bool isOn)
    {
        ExecuteCommands();
        ChangeColor(isOn);
    }

    private void ChangeColor(bool isOn)
    {
        ColorBlock colorBlock = toggle.colors;
        if (isOn)
        {
            colorBlock.normalColor = enabledColor;
            colorBlock.highlightedColor = enabledColor;
            colorBlock.selectedColor = enabledColor;
        }
        else
        {
            colorBlock.normalColor = disabledColor;
            colorBlock.highlightedColor = disabledColor;
            colorBlock.selectedColor = disabledColor;
        }
        toggle.colors = colorBlock;
    }
    #endregion
}
