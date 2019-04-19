using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    public float cost = 10f;
    public string towerName = "scout";
    public string description = "unité de base";
    public float power = 10f;
    public float radius = 10f;
    public float attackSpeed = 250f; // 1000 = 1 sec
    public List<EnemyAttribute> targetedAttributes = new List<EnemyAttribute>();
    public int upgradeIndex = 1;
    private float nextAttackTimer;
    Vector3 position;
    EnemyHealth focus;
    public GameObject projectile;

    void Start()
    {
        if(targetedAttributes.Count == 0) {
            targetedAttributes.Add(EnemyAttribute.GROUND);
            targetedAttributes.Add(EnemyAttribute.AIR);
        }
        nextAttackTimer = 0f;
        position = transform.position;
    }

    bool isInRadius(Vector3 positionEnemy, out float distance){
        distance = Vector3.Distance(position, positionEnemy);
        return distance <= radius/15;
    }
    bool isInRadius(Vector3 positionEnemy){
        float distance = 0f;
        return isInRadius(positionEnemy, out distance);
    }

    void AcquireTarget(){
        EnemyHealth closest = null;
        float closestDistance = float.MaxValue;
        EnemyHealth[] enemyHealths = GameObject.FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemyHealth in enemyHealths) {
            float distance = 0;
            if(isInRadius(enemyHealth.transform.position, out distance)){
                if(!targetedAttributes.Contains(enemyHealth.attribute)) 
                    continue;
                if(distance < closestDistance){
                    closestDistance = distance;
                    closest = enemyHealth;
                }
            }
        }
        if(closest != null){
            focus = closest;
            //nextAttackTimer = 0; // Reset
            //Debug.Log(name + " has acquired Target " + focus);
        }
    }

    void AttackTarget(){
        if(nextAttackTimer >= attackSpeed){
            nextAttackTimer = 0;
            //Debug.Log(name + " is attacking " + focus.name);
            GameObject gameProjectile = Instantiate(projectile, transform.position + new Vector3(0,1,0) , transform.rotation);
            gameProjectile.GetComponent<ProjectileAnimation>().SetTarget(focus.transform, 15f);
            
            focus.TakeDamage(power);
            if(focus.isDead) LoseFocus();
        }
    }

    void LookAtTarget(){
        Vector3 direction = focus.transform.position - transform.position;
        Quaternion rotationToward = Quaternion.LookRotation(direction);
        transform.rotation = rotationToward;
        transform.Rotate(-90f,0,0);
    }

    void LoseFocus(){
        focus = null;
        //Debug.Log(name + " lost Target");
    }

    void Update()
    {
        if(nextAttackTimer < attackSpeed) nextAttackTimer += Time.deltaTime*1000;
        if(focus == null || focus.isDead){
            AcquireTarget();
        }
        else if (isInRadius(focus.transform.position)) {
            LookAtTarget();
            AttackTarget();
        }
        else {
            LoseFocus();
        }
    }
}
