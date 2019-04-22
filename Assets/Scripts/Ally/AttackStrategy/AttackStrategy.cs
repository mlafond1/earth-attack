using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStrategy {

    protected AttackEnemy tower;
    protected EnemyHealth focus;

    public void SetTower(AttackEnemy ae){
        tower = ae;
    }

    public virtual void AcquireTarget(){
        EnemyHealth closest = null;
        float closestDistance = float.MaxValue;
        EnemyHealth[] enemyHealths = GameObject.FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemyHealth in enemyHealths) {
            float distance = 0;
            if(IsInRadius(enemyHealth.transform.position, out distance)){
                if(!tower.targetedAttributes.Contains(enemyHealth.attribute)) 
                    continue;
                if(distance < closestDistance){
                    closestDistance = distance;
                    closest = enemyHealth;
                }
            }
        }
        if(closest != null){
            focus = closest;
        }
    }

    public abstract void Attack();

    protected virtual void SpecialEffect(EnemyHealth enemy){}

    public virtual void LoseFocus(){
        focus = null;
    }

    public virtual bool HasFocus(){
        return focus != null && !focus.isDead;
    }

    public virtual bool IsFocusInRadius(){
        return HasFocus() && IsInRadius(focus.transform.position);
    }

    public virtual Vector3 GetTargetLocation(){
        return focus.transform.position;
    }

    public bool IsInRadius(Vector3 positionEnemy, out float distance){
        distance = Vector3.Distance(tower.transform.position, positionEnemy);
        return distance <= tower.radius/15;
    }
    public bool IsInRadius(Vector3 positionEnemy){
        float distance = 0f;
        return IsInRadius(positionEnemy, out distance);
    }

}