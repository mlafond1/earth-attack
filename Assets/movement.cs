using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 target;
    private int wavepointIndex = 0;
    private bool hasTarget = false;
    // Start is called before the first frame update
    void Start()
    {
        if(Waypoints.isReady){
            target = Waypoints.points[0];
            hasTarget = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasTarget){
            if(!Waypoints.isReady) return;
            target = Waypoints.points[0];
            hasTarget = true;
        }
        Vector3 dir = target - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target) <= 0.5f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}

