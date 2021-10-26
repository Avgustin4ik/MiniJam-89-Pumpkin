using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameRules : MonoBehaviour
{


    public GameEvent OnLooseGame;
    public GameEvent OnLevelUp;

    public int _score;
    private bool isHealthDown;
    [SerializeField] private float _fallingSpeed;
    [SerializeField] private float _speedMultiply;
    [SerializeField] private float _timerReducer;
    public float timerReducer => _timerReducer;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private int _lifesCount;


    public int LifeCount
    {
        get
        {
            Debug.Log("LifeCount getter value = " + _lifesCount);
            return Mathf.Clamp(_lifesCount,0,100);
        }
        set 
        {
            Debug.Log("LifeCount setter value = " + value);
            _lifesCount = value;
            isHealthDown = _lifesCount <= 0;
            if(isHealthDown)
            {
                Tools.PrintLine("Эта линия напечатана в сеттре");
                OnLooseGame?.Invoke();
                LooseGame();
            }
        }
    }
    public int GetLifesCount() => _lifesCount;
    public float GetFallingSpeed() => _fallingSpeed;
    
    private Camera mainCamera;
    [SerializeField] private int _spawnPointCount;
    public int SpawnPointCount
    {
        get {return _spawnPointCount;}
        set  {_spawnPointCount = Mathf.Clamp(value,3,9);}
    }
    public int Level;
       public float width,hight,leftBorder,rightBorder,step;

    [SerializeField] private int _gameFPS;
    
    void Awake()
    {   
        if(SpawnPointCount==0) 
            SpawnPointCount = 4;
        CalculateGeometry();
        Application.targetFrameRate = _gameFPS;
    }
    
    public void PauseGame ()
    {
        Time.timeScale = 0f;
        Tools.PrintLine("GAME IS PAUSED");
    }

    public void ResumeGame()
    {
        Tools.PrintLine("GAME IS RESUMED");
        Time.timeScale = 1;
    }

    void CalculateGeometry()
    {

        mainCamera = Camera.main;
        hight = mainCamera.orthographicSize * 2f;
        width = hight * mainCamera.aspect;
        step = width/(float)SpawnPointCount; //проверить необходимость обертки
        leftBorder = 0f - width/2f + step/2f;
        rightBorder = 0f + width/2f - step/2f;
        
    }

    public void LevelUp()
    {
        Tools.PrintLine("Level up");
        Level++;
        OnLevelUp?.Invoke();
        _fallingSpeed *= _speedMultiply;//тут ошибка, сначала должны были обновиться все числа в этом скрипте, а потом запускаться событие и передавать все в другие скрипты
    }

    void OnDrawGizmos()
    {
        float y = 100;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftBorder,-y,0),new Vector3(leftBorder,y,0));
        Gizmos.DrawLine(new Vector3(rightBorder,-y,0),new Vector3(rightBorder,y,0));
    }

    public void AddScore(int count = 10)
    {
        Tools.PrintLine("добавление очков");
        // if(count == 0) count = 10;
        if (count <= 0) 
            throw new InvalidOperationException();
        _score += count;
        if (_score % 50 == 0)
            LevelUp();

    }
    

    public void TakeDamage()
    {
        LifeCount--;
        healthBar.GetComponent<HealthBar>().TakeDamage();
    }

    public void LooseGame()
    {
        Tools.PrintLine("Игра проиграна! ВСЕ!");
        //загрузка финального экрана 
        FindObjectOfType<GameManager>().UpdateGameState(GameState.EndGame);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
   

}
