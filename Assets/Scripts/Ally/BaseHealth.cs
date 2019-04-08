using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    Collider baseCollider;
    int health;
    void Start()
    {
        health = 15;
    }

    public void TakeDamage(){
        --health;
        if(health == 0) Death();
    }

    private void Death(){
        Debug.Log("Partie perdue");
    }
    
}
