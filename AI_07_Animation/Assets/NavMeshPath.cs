using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPath : MonoBehaviour
{
    Move move;
    SteeringSeek seek;

    public GameObject target;
    private UnityEngine.AI.NavMeshPath path;
    

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
        seek = GetComponent<SteeringSeek>();

        path = new UnityEngine.AI.NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path);

        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);

        if (path.corners.Length >= 1)
        {
            seek.Steer(path.corners[1]);
        }
        else
        {
            seek.Steer(transform.position);
        }

     
    }
}
