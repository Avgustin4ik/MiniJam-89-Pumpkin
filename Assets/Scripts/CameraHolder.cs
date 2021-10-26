using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _strength;
    
    public void Shake()
    {
       Camera.main.DOShakePosition(0.5f,1f);  
    }
}
