using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{



    [SerializeField] private ObjectPoolBehaviour _projectileObjectPool;
    private List<Vector3> _spawnPositions = new List<Vector3>();
    [SerializeField] private float _spawnTimerSec;
    [SerializeField] private float _ySpawnPosition;
    private GameRules GAMERULES;
    private float timerReducer;
    public bool isShakeble;


    void Start()
    {

        GAMERULES = GameObject.FindObjectOfType<GameRules>();    
        StartCoroutine(SpawnTimer(_spawnTimerSec));
        timerReducer = GAMERULES.timerReducer;
        // float xPos = 0f - GAMERULES.width/2f + GAMERULES.step/2f;
        float xPos = GAMERULES.leftBorder;
        _spawnPositions.Add(new Vector3(xPos,_ySpawnPosition,0));
        for (int i = 1; i < GAMERULES.SpawnPointCount; i++)
        {
            xPos += GAMERULES.step;
            _spawnPositions.Add(new Vector3(xPos,_ySpawnPosition,0));
        }
    }  

    public List<Vector3> GetSpawnPoints()
    {
        return _spawnPositions;
    }

    private Vector3 GetSpawnPosition() => _spawnPositions[Random.Range(0,_spawnPositions.Count - 1)];

    void SpawnProjectile()
    {
        StopAllCoroutines();
        Vector3 pos = _spawnPositions[Random.Range(0,_spawnPositions.Count - 1)];
        GameObject newProjectile = _projectileObjectPool.GetPooledObject();
        newProjectile.transform.position = GetSpawnPosition();
        newProjectile.SetActive(true);
        StartCoroutine(SpawnTimer(_spawnTimerSec));
    }

    IEnumerator SpawnTimer(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        SpawnProjectile();
    }

    public void ShakeScreen(Vector2 direction)
    {
        if (!isShakeble) return;
        Tools.PrintLine("Тряска экрана. Смещение всех прожектайлов");
        var listOfProjectiles = _projectileObjectPool.GetPooledObjects();
        foreach (var item in listOfProjectiles)
        {
            if  (item.transform.position.x <= GAMERULES.leftBorder && direction.x < 0 ||
                 item.transform.position.x >= GAMERULES.rightBorder && direction.x > 0)
            {
                item.transform.position -= new Vector3(direction.x,direction.y)*GAMERULES.step*(GAMERULES.SpawnPointCount - 1);
            }
            else           
                item.transform.position += new Vector3(direction.x,direction.y)*GAMERULES.step;//заменить шаг
        }
    }    

    void OnDrawGizmos() {
        if (_spawnPositions != null)
        {
            foreach (var spawnPoint in _spawnPositions)
            {
                Gizmos.DrawSphere(spawnPoint,5f);
            }
        }
    }

    public void ReduceSpawnDelay() 
    {
        if (isShakeble)
            _spawnTimerSec /= timerReducer;
    } 
}
