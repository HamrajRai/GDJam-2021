using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float stoppingDistance = 1.0f;
    [SerializeField] float attackingDistance = 5.0f;

    [Header("References")]
    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] WeaponManager weaponManager = null;
    GameObject p = null;
    public static int numActiveEnemies = 0;

    private void Start()
    {
        p = FindObjectOfType<PlayerController>().gameObject;
        numActiveEnemies++;
    }
    private void OnDisable() {
        numActiveEnemies--;
    }
    private void Update()
    {
        var dist = (p.transform.position - transform.position).magnitude;
        if (dist >= stoppingDistance){
            agent.isStopped = false;
            agent.SetDestination(p.transform.position);
        }
        else
            agent.isStopped = true;
        if (dist <= attackingDistance)
            weaponManager.AttackWithCurrentWeapon();
    }
}
