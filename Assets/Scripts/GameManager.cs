using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameRules _gameRules;
    InputControls input;
    public static GameManager Instance;
    public GameState State;
    public static Action<GameState> OnGameStateChanged;

    public float _startScreenTimer = 10f; 
    void OnDestroy()
    {
        input.CharacterControls.AnyKey.Disable();
    }
    void Awake() 
    {
        if (Instance == null)
            Instance = this;
        else if(Instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        UpdateGameState(GameState.StartGame);
        input = new InputControls();
        input.CharacterControls.AnyKey.Enable();
        input.CharacterControls.AnyKey.performed += OnAnyKeyInput;
    }

    public void UpdateGameState(GameState newState)
    {
        Debug.Log("STATE HAS BEEN CHANGED ON : " + newState);
        State = newState;
        switch(newState)
        {
            case GameState.StartGame:
                HandleStartScreen();
                break;
            case GameState.MainGame:
                HandleMainScreen();
                break;
            case GameState.EndGame:
                HandleLoseScreen();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }
        OnGameStateChanged?.Invoke(newState);
    }

    void OnAnyKeyInput(InputAction.CallbackContext context)
    {
        Debug.Log( "Input shit is working ///  " + context.ReadValueAsButton());
        UpdateGameState(GameState.MainGame);
    }

    private void HandleLoseScreen()
    {
        _gameRules.PauseGame();
        input.CharacterControls.Disable();
    }
    private void HandleStartScreen()
    {
        _gameRules.PauseGame();
        StartCoroutine(StartScreenTimer(_startScreenTimer));
    }

    IEnumerator StartScreenTimer(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        if (State == GameState.StartGame)
            UpdateGameState(GameState.MainGame);
    }

    private void HandleMainScreen()
    {
        _gameRules.ResumeGame();
        input.CharacterControls.Enable();
    }
}

public enum GameState{
    StartGame,
    MainGame,
    EndGame
}


