using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [Tooltip("view cone in degrees which the enemy can find the player")]
    [SerializeField] float searchFOV = 60;

    [Tooltip("Max view distance enemy can see")]
    [SerializeField] float searchDist = 10;

    [SerializeField] float originHeight;

    [SerializeField] float attackDist = 5.0f;

    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] GameObject PointHolder;
    [SerializeField] WeaponManager weaponManager = null;

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

        foreach (var point in points)
            print(point.position);
    }

    bool spotted = false;
    private void Update()
    {

        if (Vector3.Distance(transform.position, enemyAgent.destination) <= originHeight + 0.1f)
            enemyAgent.SetDestination(points[rand.Next(1, points.Length - 1)].position);


        var playerDir = (player.position - transform.position).normalized;
        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(transform.forward, playerDir));


        if (spotted)
            enemyAgent.SetDestination(player.position);
        else if (searchFOV * .5f > angle)
        {
            var cast = Physics.RaycastAll(transform.position + transform.up, playerDir, searchDist);

            if (cast.Length > 0)
            {
                cast.OrderBy(hit => hit.distance);
                print(cast[0].transform.name);

                if (cast[0].transform.name == player.name)
                    spotted = true;
            }
        }

        if ((player.position - transform.position).magnitude < attackDist)
            weaponManager.AttackWithCurrentWeapon();

    }

}
