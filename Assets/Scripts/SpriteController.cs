using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpriteController : MonoBehaviour
{
    [Header("Sprites coolection")]
    [SerializeField] private Sprite _defaultSpreite;
    [SerializeField] private Sprite _hitWallSprite;
    [SerializeField] private Sprite _defenceSprite;
    [Header("Tween setup")]
    [SerializeField] private float _duration;
    [SerializeField] private int _loops;


    private SpriteRenderer _render;
    // Start is called before the first frame update
    void Start()
    {
        _render = GetComponent<SpriteRenderer>();
        _render.sprite = _defaultSpreite;  
    }
    public void StartRenderCoroutine(Vector3 direction)
    {
        StartCoroutine(HitWallRender(direction));
    }
   
    public IEnumerator HitWallRender(Vector3 direction)
    {
        _render.sprite = _hitWallSprite;
        _render.flipX = direction.x > 0;
        yield return new WaitForSecondsRealtime(_duration);
        _render.sprite = _defaultSpreite;

    
    }
}
