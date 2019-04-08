using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{

    float attackRange;
    float attackDamage;
    float attackRate;
    float nextAttackTimer;
    Vector3 position;
    EnemyHealth focus;
    public Transform projectile;

    // Start is called before the first frame update
    void Start()
    {
        attackRange = 10f;
        attackDamage = 10f;
        attackRate = 0.25f;  // 1 = 1 sec
        nextAttackTimer = 0f;
        position = transform.position;
    }

    bool isInRadius(Vector3 positionEnemy, out float distance){
        distance = Vector3.Distance(position, positionEnemy);
        return distance <= attackRange;
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
                if(distance < closestDistance){
                    closestDistance = distance;
                    closest = enemyHealth;
                }
            }
        }
        if(closest != null){
            focus = closest;
            nextAttackTimer = 0;
            Debug.Log(name + " has acquired Target " + focus);
        }
    }

    void AttackTarget(){
        nextAttackTimer += Time.deltaTime;
        if(nextAttackTimer >= attackRate){
            nextAttackTimer = 0;
            Debug.Log(name + " is attacking " + focus.name);
            GameObject gameProjectile = Instantiate(projectile.gameObject, transform.position + new Vector3(0,1,0) , transform.rotation);
            gameProjectile.GetComponent<ProjectileAnimation>().SetTarget(focus.transform, 15f);
            
            bool isDead = false;
            focus.TakeDamage(attackDamage, out isDead);
            if(isDead) LoseFocus();
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
        Debug.Log(name + " lost Target");
    }

    void Update()
    {
        if(focus == null){
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
