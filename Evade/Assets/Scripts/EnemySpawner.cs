using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour {
    private float spawnRate = 5f;
    private bool canSpawn = true;
    
    [SerializeField]
    private GameObject[] enemyPrefabs;

    private IEnumerator Spawner() {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn) {
            yield return wait;
            var currentEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(currentEnemy, transform.position, Quaternion.identity);
        }
    }

    private void Start() {
        GameEvents.Killed.AddListener(StopSpawn);
        StartCoroutine(Spawner());
    }

    private void StopSpawn() {
        Destroy(this.gameObject);
    }
}