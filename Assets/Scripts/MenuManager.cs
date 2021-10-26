using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public GameObject StartScreen;
    public GameObject MainScreen;
    public GameObject LoseScreen;

    
    void Start() 
    {
        if (Instance == null)
            Instance = this;
        else if(Instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerChangedState;    
        
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerChangedState;    
    }
    private void GameManagerChangedState(GameState newState)
    {
        StartScreen.SetActive(newState == GameState.StartGame);
        MainScreen.SetActive(newState == GameState.MainGame);
        LoseScreen.SetActive(newState == GameState.EndGame);
            

    }

    
}
