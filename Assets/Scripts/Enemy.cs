using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _fallingSpeed;
    [SerializeField] private float _secondsToLive;
    [SerializeField] private float _minY;
    
    private GameRules GAMERULES;
    [Header("Random direction controll")]
    [SerializeField] private AnimationCurve _randomLawCurve;
    [SerializeField] private float _zeroBorderY,_zeroBorderX;
    private Vector3 _direction
    {
        get
        {
            return CalculateRandomDirection();
        }
        set
        {
            // _direction = value;
        }
    }


    private Vector3 CalculateRandomDirection()
    {
        float randomX = Random.value;
        float randomY = _randomLawCurve.Evaluate(randomX);
        Vector3 leftStep = Vector3.left*GAMERULES.step;
        Vector3 rightStep = Vector3.right*GAMERULES.step;
        Vector3 direction = (randomY > _zeroBorderY) ? Vector3.down : ((randomX > _zeroBorderX) ? rightStep : leftStep); 
        if (transform.position.x <= GAMERULES.leftBorder && direction == leftStep)
            direction = Vector3.down;
        if (transform.position.x >= GAMERULES.rightBorder && direction == rightStep)
            direction = Vector3.down;
        if(transform.position.y <= _minY)
        {
            direction = new Vector3(0,-3f,0);
        }
        return direction;
    }
    
    
    [SerializeField] GameEvent _onHitPlayer;

    // Start is called before the first frame update
    void Start()
    {
        GAMERULES = GameObject.FindObjectOfType<GameRules>();  
        
    }

    void OnEnable() 
    {
        StartCoroutine(TimerToAutoRemoveProjectile());
    }

    void FixedUpdate()
    {
        // if(transform.position.x<=GAMERULES.leftBorder || transform.position.x >= GAMERULES.rightBorder)
        //     _direction = Vector3.down;
        Move(_direction);
    }

    void Move(Vector3 direction)
    {

        transform.position += direction*_fallingSpeed;

    }

    IEnumerator TimerToAutoRemoveProjectile()
    {
        yield return new WaitForSecondsRealtime(_secondsToLive);
        RemoveProjectile();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            {
                Tools.PrintLine("Бэтмен попал в мишень");
                _onHitPlayer?.Invoke();
                RemoveProjectile();
            }    
    }

    void RemoveProjectile()
    {
        gameObject.SetActive(false);
    }

    
}
