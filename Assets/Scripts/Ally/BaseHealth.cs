﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    private int health;
    public int maxHealth;
    public Canvas heatlhContainer;
    public Image healthBar;
    void Start()
    {
        if(maxHealth == 0) maxHealth = 15;
        health = maxHealth;
        heatlhContainer.GetComponentInChildren<Text>().text = health + " / " + maxHealth;
        HealthBarLookAtCamera();
    }

    public void TakeDamage(){
        --health;
        healthBar.fillAmount = (float)health/maxHealth;
        heatlhContainer.GetComponentInChildren<Text>().text = (health > 0 ? health : 0) + " / " + maxHealth;
        if(health == 0) Death();
    }

    private void Death(){
        GameObject.FindObjectOfType<EndGame>().Lose();
    }
    
    public void HealthBarLookAtCamera(){
        Vector3 lookOrientation = Camera.main.transform.position;
        lookOrientation.z = heatlhContainer.transform.position.z;
        heatlhContainer.transform.LookAt(lookOrientation);
        heatlhContainer.transform.Rotate(180,0,0);
    }

}
