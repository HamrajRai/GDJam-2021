//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    Transform[] points;
    [SerializeField] GameObject PointHolder;
    // [SerializeField] float speed;
    System.Random rand = new System.Random();
    NavMeshAgent playerAgent;
    Vector3 nextPosition;

    private void Start()
    {
        points = PointHolder.transform.GetComponentsInChildren<Transform>();
        nextPosition = transform.position;
        playerAgent = GetComponent<NavMeshAgent>();
        playerAgent.updateRotation = true;
        playerAgent.updateUpAxis = true;
        playerAgent.autoBraking = true;
    }

    private void Update()
    {

        if (nextPosition == transform.position)
        {
            playerAgent.CalculatePath(nextPosition = points[rand.Next(0, points.Length - 1)].position, GetComponent<NavMeshAgent>().path);

            print("move fat saa");
        }

        // playerAgent.velocity = playerAgent.velocity.normalized * speed;

    }

}
