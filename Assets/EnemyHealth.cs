using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    float health;

    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0) Death();
    }

    public void Death(){
        Debug.Log(name + " died");
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
