using UnityEngine;

public class InPlaceAoeAttack : RangedAoeAttack{


    public InPlaceAoeAttack(float radius=5f) : base(radius){
        
    }

    protected override bool IsInAreaOfEffect(EnemyHealth e){
        return IsInRadius(e.transform.position);
    }

}