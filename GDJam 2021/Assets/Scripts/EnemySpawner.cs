using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] float spawnInterval = 1.0f;

    [Header("References")]
    [SerializeField] GameObject enemyPrefab = null;

    float _internalInterval = 0.0f;
    private void Start()
    {
        _internalInterval = spawnInterval;
    }

    private void Update()
    {
        _internalInterval -= Time.deltaTime;
        if (_internalInterval <= 0.0f)
        {
            var g = GameObject.Instantiate(enemyPrefab);
            g.transform.position = transform.position;
            _internalInterval = spawnInterval;
        }
    }

}
