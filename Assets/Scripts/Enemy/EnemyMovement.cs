using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10f;
    private float currentSpeed = 10f;
    private Vector3 target;
    private int pointIndex = 0;
    private bool hasTarget = false;
    private bool stopSignal = false;
    private bool isSlowed = false;
    
    void Start()
    {
        if(FollowPoints.isReady){
            target = FollowPoints.points[0];
            hasTarget = true;
        }
        currentSpeed = speed;
        transform.Rotate(90,0,0); // Depends on the enemy
    }

    public void ApplySlow(float slowAmount, float duration){
        if(!isSlowed)
            StartCoroutine(SlowDebuff(slowAmount, duration));
        else{
            StopCoroutine("SlowDebuff");
            currentSpeed = speed;
            StartCoroutine(SlowDebuff(slowAmount, duration));
        }
    }

    IEnumerator SlowDebuff(float slowAmount, float duration){
        isSlowed = true;
        currentSpeed -= slowAmount;
        yield return new WaitForSecondsRealtime(duration/1000);
        currentSpeed = speed;
        isSlowed = false;
    }

    public void SendStopSignal(){
        stopSignal = true;
    }

    void Update()
    {
        if(stopSignal) return;
        if(!hasTarget){
            if(!FollowPoints.isReady) return;
            target = FollowPoints.points[0];
            hasTarget = true;
        }
        Vector3 dir = target - transform.position;
        transform.Translate(dir.normalized * (currentSpeed/4.8f) * Time.deltaTime, Space.World);

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
        EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
        float nbOfFrame = 15f;
        float step = 1f/nbOfFrame;
        x *= step; y*=step; z*=step;
        float waitTime = 0.001f;
        for(int i = 0; i < nbOfFrame; ++i){
            if(stopSignal) break;
            transform.Rotate(x,y,z);
            enemyHealth.HealthBarLookAtCamera();
            yield return new WaitForSecondsRealtime(waitTime);
        }
    }
}

