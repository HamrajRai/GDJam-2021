using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] float spawnInterval = 1.0f;
    [SerializeField] int maxNumEnemies = 5;

    [Header("References")]
    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] List<Transform> spawnPositions = new List<Transform>();

    List<GameObject> _enemies = new List<GameObject>();


    float _internalInterval = 0.0f;
    private void Start()
    {
        _internalInterval = spawnInterval;
        for (int i = 0; i < maxNumEnemies; i++)
        {
            _enemies.Add(GameObject.Instantiate(enemyPrefab));
            _enemies[_enemies.Count - 1].SetActive(false);
        }
    }

    private void Update()
    {
        _internalInterval -= Time.deltaTime;
        if (_internalInterval <= 0.0f)
        {
            foreach (var e in _enemies)
                if (!e.activeSelf)
                {
                    e.SetActive(true);
                    e.transform.position = spawnPositions[Random.Range(0, spawnPositions.Count)].transform.position;
                    break;
                }


            _internalInterval = spawnInterval;
        }
    }

}
