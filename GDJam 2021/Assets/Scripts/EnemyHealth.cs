using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : Health
{
    [SerializeField] float scoreValue = 100.0f;
    [SerializeField] float knockbackTime = 1.5f;

    [Header("References")]
    [SerializeField] NavMeshAgent agent = null;
    public override void Die()
    {
        // death animation coroutine goes here
        ScoreManager.AddScore(scoreValue);
        FindObjectOfType<PowerupManager>().OnEnemyDie(transform);
        gameObject.SetActive(false);
        health = _maxHP;
    }
}
