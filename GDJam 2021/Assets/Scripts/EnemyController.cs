//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [Tooltip("view cone in degrees which the enemy can find the player")]
    [SerializeField] float searchFOV = 10;
    [SerializeField] float searchDist = 5;

    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] GameObject PointHolder;

    //
    Transform[] points;

    // [SerializeField] float speed;
    System.Random rand = new System.Random();
    NavMeshAgent enemyAgent;

    private void Start()
    {
        points = PointHolder.transform.GetComponentsInChildren<Transform>();
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.nextPosition = transform.position;
        enemyAgent.updateRotation = true;
        enemyAgent.updateUpAxis = true;
        enemyAgent.autoBraking = true;
    }

    bool spotted = false;
    private void Update()
    {
        //  print("next: " + nextPosition);
        //  print("current: " + transform.position);

        if (Vector3.Distance(transform.position, enemyAgent.destination) <= .1f)
        {
            enemyAgent.SetDestination(points[rand.Next(1, points.Length - 1)].position);

            //   playerAgent.nextPosition = (nextPosition = points[rand.Next(1, points.Length - 1)].position);
            print("move fat saa");
        }

        var playerDir = (player.position - transform.position).normalized;
        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(transform.forward, playerDir));

        print(searchFOV + " : " + angle);
        if (Physics.Raycast(transform.position, playerDir, searchDist) || spotted)
            if (searchFOV > angle)
            {
                enemyAgent.SetDestination(player.position);
                spotted = true;
            }
        // playerAgent.velocity = playerAgent.velocity.normalized * speed;

    }

}
