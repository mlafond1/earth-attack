using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAnimation : MonoBehaviour
{

    private Transform target;
    private bool hasTarget = false;
    private float speed;

    public void SetTarget(Transform newTarget, float newSpeed){
        target = newTarget;
        speed = newSpeed;
        hasTarget = true;
    }

    void Update()
    {
        if(!hasTarget) return;
        try{
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            if (Vector3.Distance(transform.position, target.position) <= 0.5f) 
                Destroy(gameObject);
        } catch (MissingReferenceException){
            DestroyImmediate(gameObject);
        }
    }
}
