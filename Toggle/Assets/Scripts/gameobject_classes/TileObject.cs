using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileObject : MonoBehaviour
{
    #region fields
    private Gameboard gameboard;
    private Button button;
    private Tile tile;
    private Color colorChange;
    private Color disabledColor;
    public List<ICommand> commands;
    public Color enabledColor;
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
        button = this.GetComponent<Button>();
        button.onClick.AddListener(ExecuteCommands);
        disabledColor = button.colors.normalColor;
    }
    #endregion

    #region methods
    public void SetupCommands()
    {
        if (gameboard != null && tile != null)
        {
            commands.Add(new ToggleCommand(gameboard, tile));
            commands.Add(new ColorChangeCommand(button, colorChange));
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
        ComputeColorChange();
        ColorChangeCommand colorChangeCommand = new ColorChangeCommand(button, colorChange);
        CommandManager.Instance.AddCommand(colorChangeCommand);
        colorChangeCommand.Execute();
        /*foreach (ICommand command in commands)
        {
            CommandManager.Instance.AddCommand(command);
            command.Execute();
            Debug.Log("Command executed...");
        }*/
    }

    private void ComputeColorChange()
    {
        if (tile.Enabled)
        {
            colorChange = disabledColor;
        }
        else
        {
            colorChange = enabledColor;
        }
    }
    #endregion
}
