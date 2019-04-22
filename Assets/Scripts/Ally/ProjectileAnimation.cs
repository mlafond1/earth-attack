using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAnimation : MonoBehaviour
{

    private Transform target;
    private bool hasTarget = false;
    private float speed;
    private Vector3 lastPosition;
    private float acceleration;

    public void SetTarget(Transform newTarget, float newSpeed){
        target = newTarget;
        lastPosition = target.position;
        speed = newSpeed;
        hasTarget = true;
        acceleration = 1;
        if(target == null) DestroyImmediate(gameObject);
    }

    public void SetTarget(Vector3 newTarget, float newSpeed){
        lastPosition = newTarget;
        speed = newSpeed;
        hasTarget = true;
        acceleration = 1;
    }

    void Update()
    {
        if(!hasTarget) return;
        if(target != null){
            lastPosition = target.position;
        }
        Vector3 dir = lastPosition - transform.position;
        transform.Translate(dir.normalized * speed * acceleration * Time.deltaTime, Space.World);
        acceleration += Time.deltaTime * 2;
        if (Vector3.Distance(transform.position, lastPosition) <= 0.5f)
            Destroy(gameObject);
    }
}
