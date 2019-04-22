using UnityEngine;

public class RangedAoeSlowAttack : RangedAoeAttack{

    private float slowAmount;
    private float slowDuration;

    public RangedAoeSlowAttack(float slowAmount=15f, float slowDuration=1000f, float radius=10f) : base(radius){
        this.slowAmount = slowAmount;
        this.slowDuration = slowDuration;
    }

    protected override void SpecialEffect(EnemyHealth enemy){
        enemy.gameObject.GetComponent<EnemyMovement>().ApplySlow(slowAmount, slowDuration);
    }

}