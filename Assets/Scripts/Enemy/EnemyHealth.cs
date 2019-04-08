using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    float health;

    public void TakeDamage(float damage, out bool isDead){
        health -= damage;
        isDead = health <= 0;
        if(isDead) Death();
    }

    public void Death(){
        Debug.Log(name + " died");
        Destroy(gameObject);
    }

    void Start()
    {
        health = 100f;
    }

}
