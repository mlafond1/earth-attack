﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private float health;
    public float maxHealth;
    public float value;
    public EnemyAttribute attribute = EnemyAttribute.NONE;
    public Canvas heatlhContainer;
    public Image healthBar;

    void Start()
    {
        if(maxHealth == 0) maxHealth = 100f;
        if(attribute == EnemyAttribute.NONE) attribute = EnemyAttribute.GROUND;
        health = maxHealth;
        value = 0;
        HealthBarLookAtCamera();
    }

    public void SetMaxHealth(float max){
        maxHealth = max;
        health = maxHealth;
    }

    public void TakeDamage(float damage, out bool isDead){
        health -= damage;
        healthBar.fillAmount = health/maxHealth;
        isDead = health <= 0;
        if(isDead) Death();
    }

    public void Death(){
        //Debug.Log(name + " died");
        gameObject.GetComponent<EnemyMovement>().SendStopSignal();
        Destroy(gameObject, 1);
    }

    public void HealthBarLookAtCamera(){
        Vector3 lookOrientation = Camera.main.transform.position;
        lookOrientation.z = transform.position.z;
        heatlhContainer.transform.LookAt(lookOrientation);
    }

}
