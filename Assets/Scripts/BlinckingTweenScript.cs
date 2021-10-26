using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using UnityEngine.UI;


public class BlinckingTweenScript : MonoBehaviour
{
    private Text _text;
    [SerializeField] private float _duration;
    [SerializeField] private int _loopCount;
    [SerializeField] private Ease _ease;
    private int maxLoopCount,minLoopCount;
    private float maxDuration,minDuration;
    private Color defaultColor;

    void Awake()
    {
        defaultColor = Color.white;
        defaultColor.a = 0;
        _text = GetComponent<Text>();
        _text.color = defaultColor;
        maxDuration = 4;
        maxDuration = 3;    
        minLoopCount = 1;
        minDuration = 0.25f;
    }
    public void Blink()
    {
        _text.DOFade(1f,Mathf.Clamp(_duration,minDuration,maxDuration)).SetLoops(2).SetEase(_ease).OnComplete(() => _text.color = defaultColor);
    }
}
