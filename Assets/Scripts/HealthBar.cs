using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public List<GameObject> healthBar;
    private GameRules GAMERULES;
    [SerializeField] private GameObject _leftHart;
    [SerializeField] private GameObject _rigthHart;
    [SerializeField] private float _xMinStep;
    [SerializeField] private float _xMaxStep;



    // Vector3 GetSpawnPosition()
    // Start is called before the first frame update
    void Awake()
    {
        Vector3 position = transform.position;
        GAMERULES = GameObject.FindObjectOfType<GameRules>();
        for (int i = 0; i < GAMERULES.GetLifesCount(); i++)
        {
            
            GameObject obj = (GameObject)Instantiate(i%2 == 0 ? _leftHart : _rigthHart,this.transform);
            float step = i%2 == 0 ? _xMaxStep : _xMinStep;
            position += Vector3.right*step;
            obj.transform.position = position;
            healthBar.Add(obj);   
        }
        
    }

    public void TakeDamage()
    {   
        Debug.Log(GAMERULES.GetLifesCount());
        int x = GAMERULES.GetLifesCount();
        healthBar[x].GetComponent<Hart>().Remove();
    }


}
