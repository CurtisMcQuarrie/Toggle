using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    #region fields
    private static CommandManager instance;
    private List<ICommand> commandBuffer = new List<ICommand>();
    #endregion

    #region properties
    public static CommandManager Instance {
        get
        {
            if (instance == null)
            {
                Debug.LogError("The CommandManager is NULL.");
            }
            return instance;
        }
    }
    #endregion

    #region monobehaviour
    void Awake()
    {
        instance = this;
    }
    #endregion

    public void AddCommand(ICommand command)
    {
        commandBuffer.Add(command);
    }

    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        Debug.Log("Playing...");
        foreach (var command in commandBuffer)
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
        foreach (var command in Enumerable.Reverse(commandBuffer))
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
        commandBuffer.Clear();
    }
}
