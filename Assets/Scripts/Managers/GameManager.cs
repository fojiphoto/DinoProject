using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Stack<GameState> gameStateStack;

    private Stack<GameState> previousState;

    public delegate void OnStateChanged(GameState state);
    public static event OnStateChanged OnStateChangedEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            gameStateStack = new Stack<GameState>();
            gameStateStack.Push(GameState.MAINMENU);

            previousState = new Stack<GameState>();
            previousState.Push(GameState.MAINMENU);
        }
    }

    public void ChangeGameState(GameState state)
    {
        if (state == GetPreviousState)
        {
            BackState();
        }
        else
        {
            previousState.Push(GetCurrentState());
            gameStateStack.Push(state);
            OnStateChangedEvent(state);
        }
    }

    private void OnStateChangeInternal(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.LOADING:
         //       AdCalls.instance.Admob_Unity();
                break;
        }
    }

    public GameState GetCurrentState()
    {
        return gameStateStack.Peek();
    }

    public GameState GetPreviousState
    {
        get
        {
            return previousState.Peek();
        }
    }

    public void BackState()
    {
        var currentState = GetCurrentState();
        if (currentState == GameState.MAINMENU)
        {
            ChangeGameState(GameState.GAMEQUIT);
        }
        else if (currentState == GameState.GAMEPLAY)
        {
            ChangeGameState(GameState.PAUSED);
        }
        else if (gameStateStack.Count > 0)
        {
            gameStateStack.Pop();
            if(previousState.Count>0)
                previousState.Pop();
            OnStateChangedEvent(GetCurrentState());
        }
    }

    public AsyncOperation LoadScene(string sceneName)
    {
        return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }

    private void OnApplicationQuit()
    {
        PreferenceManager.SavePrefs();
    }
}