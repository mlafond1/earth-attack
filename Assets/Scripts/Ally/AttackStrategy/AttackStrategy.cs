using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStrategy {

    protected Tower tower;
    protected EnemyHealth focus;

    public void SetTower(Tower tower){
        this.tower = tower;
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

    public virtual void PlayAnimation(){
        GameObject gameProjectile = GameObject.Instantiate(tower.projectile, tower.transform.position + new Vector3(0,2,0) , tower.transform.rotation);
        if(tower.towerName.StartsWith("scout")) gameProjectile.transform.Rotate(-90,0,0);
        gameProjectile.GetComponent<ProjectileAnimation>().SetTarget(focus.transform, 15f);
        Animator animator = tower.GetComponent<Animator>();
        if(animator != null){
            animator.Play("Default Take");
            animator.Play("ArmatureAction");
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