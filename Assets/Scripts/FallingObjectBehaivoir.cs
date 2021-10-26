using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class FallingObjectBehaivoir : MonoBehaviour
{   

    [SerializeField] private float _fallingSpeed;
    [SerializeField] private float _secondsToLive;
    private bool isOnFlor;
    private Color _defaultColor;
    private GameRules GAMERULES;
    [SerializeField] GameEvent _onFail;
    [SerializeField] GameEvent _onSuccess;

    private Vector3 _direction
    {
        get
        {
            return Vector3.down;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {

       
        StartCoroutine(TimerToAutoRemoveProjectile());
    }
    void OnEnable() {
        GetComponent<SpriteRenderer>().color = Color.white;
        isOnFlor = false;
        Tools.PrintLine("Меняеем цвет");
        GAMERULES = GameObject.FindObjectOfType<GameRules>();
        
    }

    void Start()
    {
        _fallingSpeed = GAMERULES.GetFallingSpeed();
        if(_fallingSpeed == 0)
        {
            Tools.PrintLine("Скорость падения равна 0. Скорость переназначена");
            _fallingSpeed = 0.5f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isOnFlor) Move(_direction);
       
    }

    void Move(Vector3 direction)
    {
        transform.position += direction*_fallingSpeed;
    }

   

    IEnumerator TimerToAutoRemoveProjectile()
    {
        yield return new WaitForSeconds(_secondsToLive);
        RemoveProjectile();
    }

    void RemoveProjectile()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Finish"))
        {
            FailLogicCalculate();
        }
        if(other.CompareTag("Player"))
        {
            SuccessLogicCalculate();
        }
    }

    void SuccessLogicCalculate()
    {
        _onSuccess?.Invoke();
        Tools.PrintLine("Конфетка поймана через тригер");
        RemoveProjectile();
    }
    void FailLogicCalculate()
    {
        Debug.Log("БИЛД! КОНФЕТА НА ПОЛУ");
        Tools.PrintLine("Конфетка упала на пол");
        isOnFlor = true;
        //TakeDamage();
        GetComponent<SpriteRenderer>().DOFade(0,0.25f).SetLoops(4).OnComplete(RemoveProjectile);
        _onFail?.Invoke();
    }

    public void CheckFallingSpeed() => _fallingSpeed *= 1.5f;

    
    
}
