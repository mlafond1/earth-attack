using UnityEngine;

public class RangedAoeAttack : AttackStrategy{

    protected float aoeRadius;

    public RangedAoeAttack(float radius=10f){
        aoeRadius = radius;
    }

    public override void Attack(){
        GameObject gameProjectile = GameObject.Instantiate(tower.projectile, tower.transform.position + new Vector3(0,1,0), tower.transform.rotation);
        gameProjectile.GetComponent<ProjectileAnimation>().SetTarget(focus.transform, 15f);
        EnemyHealth[] enemies = GameObject.FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemy in enemies){
            if(!tower.targetedAttributes.Contains(enemy.attribute)) continue;
            if(IsInAreaOfEffect(enemy)){
                enemy.TakeDamage(tower.power);
                SpecialEffect(enemy);
            }
        }
        if(focus.isDead) LoseFocus();
    }

    protected virtual bool IsInAreaOfEffect(EnemyHealth e){
        float distance = Vector3.Distance(focus.transform.position, e.transform.position);
        return distance <= aoeRadius;
    }

}