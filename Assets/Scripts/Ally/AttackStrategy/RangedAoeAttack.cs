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
        DisplayAoeRadius();
        if(focus.isDead) LoseFocus();
    }

    protected virtual void DisplayAoeRadius(){
        var zoneTemplate = GameObject.Find("AttackZone");
        var attackZone = GameObject.Instantiate(zoneTemplate, GetTargetLocation()+ new Vector3(0,0.601f,0), zoneTemplate.transform.rotation);
        float r = 2*GetAoeRadius();
        attackZone.GetComponentInChildren<UnityEngine.UI.Image>().transform.localScale = new Vector3(r,r,r);
        GameObject.Destroy(attackZone, 0.5f);
    }

    protected virtual bool IsInAreaOfEffect(EnemyHealth e){
        float distance = Vector3.Distance(focus.transform.position, e.transform.position);
        return distance <= aoeRadius;
    }

    protected virtual float GetAoeRadius(){
        return aoeRadius;
    }

}