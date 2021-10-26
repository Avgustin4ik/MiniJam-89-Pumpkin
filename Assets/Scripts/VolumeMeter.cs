using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class VolumeMeter : MonoBehaviour
{
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    [SerializeField] private Slider slider;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _volumeParameter = "MasterVolume";
    public const float _firstBorder = -20f; 
     

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1f;
        ChangeVolumeLevel();
    }

    public void ChangeVolumeLevel()
    {
        float value = slider.value;
        Debug.Log(value);
        level1.SetActive(value >= 1);
        level2.SetActive(value == 2);
        ChangeAudioLevel(value);
    }

    private void ChangeAudioLevel(float value)
    {
        if(value == 2f)
            _mixer.SetFloat(_volumeParameter,0);
        if(value == 1f)
            _mixer.SetFloat(_volumeParameter,_firstBorder);
        if(value == 0f)
            _mixer.SetFloat(_volumeParameter,-80f);        
    }
}
