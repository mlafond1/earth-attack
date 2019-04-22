using UnityEngine;

public class NormalAttack : AttackStrategy{

    public override void Attack(){
        PlayAnimation();
        focus.TakeDamage(tower.power);
        SpecialEffect(focus);
        if(!HasFocus()) LoseFocus();
    }
}