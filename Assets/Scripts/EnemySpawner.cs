using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    private const float SpawnRate = 5f;
    private readonly bool _canSpawn = true;

    [SerializeField]
    private GameObject[] enemyPrefabs;

    private IEnumerator Spawner() {
        WaitForSeconds wait = new WaitForSeconds(SpawnRate);

        while (_canSpawn) {
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