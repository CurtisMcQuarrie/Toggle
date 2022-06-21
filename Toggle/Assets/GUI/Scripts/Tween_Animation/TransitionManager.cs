using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    #region fields

    [Header("Canvas")]
    public GameObject mainMenuCanvas;
    public GameObject difficultySelectCanvas;
    public GameObject settingsCanvas;
    public GameObject winCanvas;
    public GameObject gameboardCanvas;
    public GameObject infoCanvas;
    public GameObject howToCanvas;
    
    [Header("TransitionTweens")]
    public TweenTranslater transitioner;

    private List<Tuple<GameObject, GameObject>> transitionScenarios;
    private List<string> transitionScenarioStrings;
    private GameObject previousCanvas;
    private GameObject currentCanvas;

    #endregion

    #region monobehaviour

    void Awake()
    {
        transitionScenarios = new List<Tuple<GameObject, GameObject>>();
        transitionScenarioStrings = new List<string>();
    }

    void Start()
    {
        SetupTransitionScenarios();
    }

    #endregion

    #region transitions

    public void Transition(String scenario)
    {
        int scenarioIndex = transitionScenarioStrings.FindIndex(s => s.Equals(scenario));
        if (scenarioIndex >= 0)
        {
            currentCanvas = transitionScenarios[scenarioIndex].Item2;
            previousCanvas = transitionScenarios[scenarioIndex].Item1;
            StartCoroutine(TweenTransition(scenarioIndex));
        }
        else
        {
            Debug.LogWarning("Transition Scenario " + scenario + " could not be found.");
        }
    }

    public void CloseDifficultySelect()
    {
        if (Equals(previousCanvas, mainMenuCanvas))
        {
            Transition("DifficultySelectToMainMenu");
        }
        else if (Equals(previousCanvas, gameboardCanvas))
        {
            Transition("DifficultySelectToGameboard");
        }
        else if (Equals(previousCanvas, winCanvas))
        {
            Transition("DifficultySelectToWin");
        }
        else
        {
            Debug.LogWarning("Cannot find previousCanvas.");
        }
    }

    #endregion

    #region setup

    private void SetupTransitionScenarios()
    {
        currentCanvas = mainMenuCanvas;

        transitionScenarios.Add(new Tuple<GameObject, GameObject>(mainMenuCanvas, difficultySelectCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(mainMenuCanvas, settingsCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(mainMenuCanvas, infoCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(infoCanvas, mainMenuCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(mainMenuCanvas, howToCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(howToCanvas, mainMenuCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(difficultySelectCanvas, mainMenuCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(difficultySelectCanvas, gameboardCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(difficultySelectCanvas, winCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(settingsCanvas, mainMenuCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(gameboardCanvas, mainMenuCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(gameboardCanvas, difficultySelectCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(gameboardCanvas, winCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(winCanvas, difficultySelectCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(winCanvas, mainMenuCanvas));
        transitionScenarios.Add(new Tuple<GameObject, GameObject>(winCanvas, gameboardCanvas));

        transitionScenarioStrings.Add("MainMenuToDifficultySelect");
        transitionScenarioStrings.Add("MainMenuToSettings");
        transitionScenarioStrings.Add("MainMenuToInfo");
        transitionScenarioStrings.Add("InfoToMainMenu");
        transitionScenarioStrings.Add("MainMenuToHowTo");
        transitionScenarioStrings.Add("HowToToMainMenu");
        transitionScenarioStrings.Add("DifficultySelectToMainMenu");
        transitionScenarioStrings.Add("DifficultySelectToGameboard");
        transitionScenarioStrings.Add("DifficultySelectToWin");
        transitionScenarioStrings.Add("SettingsToMainMenu");
        transitionScenarioStrings.Add("GameboardToMainMenu");
        transitionScenarioStrings.Add("GameboardToDifficultySelect");
        transitionScenarioStrings.Add("GameboardToWin");
        transitionScenarioStrings.Add("WinToDifficultySelect");
        transitionScenarioStrings.Add("WinToMainMenu");
        transitionScenarioStrings.Add("WinToGameboard");
    }

    #endregion

    #region ienumerators

    IEnumerator TweenTransition(int scenarioIndex)
    {
        transitioner.ResetPosition();
        transitioner.GoToNextPosition();
        yield return new WaitForSeconds(transitioner.TweenDuration);
        previousCanvas.SetActive(false);
        currentCanvas.SetActive(true);
        transitioner.GoToNextPosition();
        yield return new WaitForSeconds(transitioner.TweenDuration);
    }

    #endregion
}