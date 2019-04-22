using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
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
    public AttackStrategy attack {get; private set;}
    public GameObject projectile;

    

    void Start()
    {
        if(targetedAttributes.Count == 0) {
            targetedAttributes.Add(EnemyAttribute.NONE);
        }
        if(attack == null){
            SetAttackStrategy(TowerFactory.GetInstance().LoadAttackStrategy(towerName));
        }
        nextAttackTimer = 0f;
    }

    public void SetAttackStrategy(AttackStrategy strategy){
        attack = strategy;
        strategy.SetTower(this);
    }

    void AttackTarget(){
        if(nextAttackTimer >= attackSpeed){
            nextAttackTimer = 0;
            attack.Attack();
        }
    }

    void LookAtTarget(){
        Vector3 direction = attack.GetTargetLocation() - transform.position;
        Quaternion rotationToward = Quaternion.LookRotation(direction);
        transform.rotation = rotationToward;
        transform.Rotate(-90f,0,0);
    }

    void Update()
    {
        if(nextAttackTimer < attackSpeed) nextAttackTimer += Time.deltaTime*1000;
        if(!attack.HasFocus()){
            attack.AcquireTarget();
        }
        else if (attack.IsFocusInRadius()) {
            LookAtTarget();
            AttackTarget();
        }
        else {
            attack.LoseFocus();
        }
    }
}
