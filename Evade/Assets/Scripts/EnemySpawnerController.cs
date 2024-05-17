using System.Collections;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour {
    [SerializeField]
    private EnemySpawner[] _linearEnemySpawners;

    [SerializeField]
    private EnemySpawner[] _dynamicEnemySpawners;

    private const float ChangeSpawnRate = 5.2f;

    private int _difficulty = 1;

    private bool _canSpawn = true;

    private void Start() {
        GameEvents.DiffRaised.AddListener(RaiseDiff);
        GameEvents.Killed.AddListener(StopSpawn);
        StartCoroutine(SpawnerControl());
    }

    private void RaiseDiff() {
        _difficulty += 1;
    }

    private void StopSpawn() {
        Destroy(this.gameObject);
    }


    private IEnumerator SpawnerControl() {
        WaitForSeconds wait = new WaitForSeconds(ChangeSpawnRate);

        foreach (var spawner in _linearEnemySpawners) {
            spawner.gameObject.SetActive(false);
        }

        foreach (var spawner in _dynamicEnemySpawners) {
            spawner.gameObject.SetActive(false);
        }

        while (_canSpawn) {
            yield return wait;

            var currentLinearSpawner = _linearEnemySpawners[Random.Range(0, _linearEnemySpawners.Length)];
            var dynamicLinearSpawner = _dynamicEnemySpawners[Random.Range(0, _dynamicEnemySpawners.Length)];

            currentLinearSpawner.gameObject.SetActive(true);
            dynamicLinearSpawner.gameObject.SetActive(true);
        }
    }
}