using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private GameRules _gameRules;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _gameRules = FindObjectOfType<GameRules>();
    }
    public void SetScore()
    {
        
        _text.text = (_gameRules._score + 10).ToString();
    }
}
