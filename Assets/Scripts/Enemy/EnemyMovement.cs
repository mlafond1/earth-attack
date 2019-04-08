using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 target;
    private int pointIndex = 0;
    private bool hasTarget = false;
    void Start()
    {
        if(FollowPoints.isReady){
            target = FollowPoints.points[0];
            hasTarget = true;
        }
        transform.Rotate(90,0,0); // Depends on the enemy
    }

    void Update()
    {
        if(!hasTarget){
            if(!FollowPoints.isReady) return;
            target = FollowPoints.points[0];
            hasTarget = true;
        }
        Vector3 dir = target - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target) <= 0.5f)
        {
            GetNextFollowPoint();
        }
    }

    void GetNextFollowPoint()
    {
        if (pointIndex >= FollowPoints.points.Length -1)
        {
            Destroy(gameObject);
            GameObject.FindObjectOfType<BaseHealth>().TakeDamage();
            return;
        }
        // Depends on the enemy
        //transform.Rotate(0,0,MapHelper2.GetInstance().rotations[pointIndex]);
        //transform.Rotate(0,MapHelper2.GetInstance().rotations[pointIndex],0);
        StartCoroutine(RotateOnPath(0,MapHelper2.GetInstance().rotations[pointIndex], 0));
        ++pointIndex;
        target = FollowPoints.points[pointIndex];
    }

    IEnumerator RotateOnPath(float x, float y, float z){
        float nbOfFrame = 15f;
        float step = 1f/nbOfFrame;
        x *= step; y*=step; z*=step;
        float waitTime = 0.001f;
        for(int i = 0; i < nbOfFrame; ++i){
            transform.Rotate(x,y,z);
            yield return new WaitForSecondsRealtime(waitTime);
        }
    }
}

