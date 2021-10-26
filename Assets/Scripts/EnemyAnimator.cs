using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Sprite _sprite1;
    [SerializeField] private Sprite _sprite2;
    [SerializeField] private float _animationTimer;
    private float _lastChangeTimer;
    public bool isTimeBased;

    private SpriteRenderer spriteRenderer;
    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {

        if(Time.time > _lastChangeTimer + _animationTimer)
        {
            ChangeSprite();
            _lastChangeTimer = Time.time;
        }   
    }
    
    private void ChangeSprite()
    {
        // if(spriteRenderer.sprite == _sprite1)
        //     spriteRenderer.sprite = _sprite2;
        // else
        //     spriteRenderer.sprite = _sprite1;

        spriteRenderer.sprite = (spriteRenderer.sprite == _sprite1) ? _sprite2 : _sprite1; 
    }


    

    void OnDisable()
    {
        
    }
}
