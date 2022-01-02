using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    #region fields
    private static CommandManager _instance;
    private List<ICommand> _commandBuffer = new List<ICommand>();
    #endregion

    #region properties
    public static CommandManager Instance {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("the CommandManager is NULL.");
            }
            return _instance;
        }
    }
    #endregion

    #region monobehaviour
    void Awake()
    {
        _instance = this;
    }
    #endregion

    public void AddCommand(ICommand command)
    {
        _commandBuffer.Add(command);
    }

    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        Debug.Log("Playing...");
        foreach (var command in _commandBuffer)
        {
            command.Execute();
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Finished...");
    }

    public void Rewind()
    {
        StartCoroutine(RewindRoutine());
    }

    private IEnumerator RewindRoutine()
    {
        foreach (var command in Enumerable.Reverse(_commandBuffer))
        {
            command.Undo();
            yield return new WaitForSeconds(1f);
        }
    }

    public void Done()
    {
        //???
    }

    public void Reset()
    {
        _commandBuffer.Clear();
    }
}
