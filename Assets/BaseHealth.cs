using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    Collider baseCollider;
    int health;
    void Start()
    {
        baseCollider = GetComponent<Collider>();
        if(baseCollider == null){
            baseCollider = gameObject.AddComponent<BoxCollider>();
        }
        health = 15;
    }

    void OnCollisionEnter(Collision otherObj) {
        Debug.Log("Collision with :" + otherObj.gameObject.name);
        EnemyHealth enemy = otherObj.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null) {
            Destroy(enemy.gameObject);
            --health;
        }
        if(health == 0){
            // Partie perdue
            Debug.Log("Partie perdue");
        }
    }
}
