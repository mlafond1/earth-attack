using UnityEngine;

public class NormalAttack : AttackStrategy{

    public override void Attack(){
        GameObject gameProjectile = GameObject.Instantiate(tower.projectile, tower.transform.position + new Vector3(0,1,0) , tower.transform.rotation);
        gameProjectile.GetComponent<ProjectileAnimation>().SetTarget(focus.transform, 15f);
        focus.TakeDamage(tower.power);
        SpecialEffect(focus);
        if(focus.isDead) LoseFocus();
    }
}