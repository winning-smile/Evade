using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour {
    [SerializeField]
    private EnemySpawner[] _linearEnemySpawners;
    
    [SerializeField]
    private EnemySpawner[] _dynamicEnemySpawners;

    private float _changeSpawnRate = 5.2f;

    private int _difficulty = 1;
    
    private bool canSpawn = true;
    private void Start()
    {
        GameEvents.DiffRaised.AddListener(raiseDiff);
        GameEvents.Killed.AddListener(StopSpawn);
        StartCoroutine(SpawnerControl());
    }

    private void raiseDiff() {
        _difficulty += 1;
    }

    private void StopSpawn() {
        Destroy(this.gameObject);
    }
    
    
    private IEnumerator SpawnerControl() {
        WaitForSeconds wait = new WaitForSeconds(_changeSpawnRate);
        
        foreach (var spawner in _linearEnemySpawners) {
            spawner.gameObject.SetActive(false);
        }
        
        foreach (var spawner in _dynamicEnemySpawners) {
            spawner.gameObject.SetActive(false);
        }
        
        while (canSpawn) {
            yield return wait;
            
            var currentLinearSpawner = _linearEnemySpawners[Random.Range(0, _linearEnemySpawners.Length)];
            var dynamicLinearSpawner = _dynamicEnemySpawners[Random.Range(0, _dynamicEnemySpawners.Length)];

            currentLinearSpawner.gameObject.SetActive(true);
            dynamicLinearSpawner.gameObject.SetActive(true);
        }
    }
}
