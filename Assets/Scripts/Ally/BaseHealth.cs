using System.Collections;
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
        HealthBarLookAtCamera();
    }

    public void TakeDamage(){
        --health;
        healthBar.fillAmount = (float)health/maxHealth;
        if(health == 0) Death();
    }

    private void Death(){
        Debug.Log("Partie perdue");
    }
    
    public void HealthBarLookAtCamera(){
        Vector3 lookOrientation = Camera.main.transform.position;
        lookOrientation.x = transform.position.x;
        heatlhContainer.transform.LookAt(lookOrientation);
    }

}
