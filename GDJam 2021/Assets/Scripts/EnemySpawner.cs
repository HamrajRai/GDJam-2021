using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] float spawnInterval = 1.0f;

    [Header("References")]
    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] Transform player = null;
    [SerializeField] GameObject patrolpoints = null;


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

            g.GetComponent<EnemyController>().setPlayer(player);
            g.GetComponent<EnemyController>().setPoints(patrolpoints);

            _internalInterval = spawnInterval;
        }
    }

}
