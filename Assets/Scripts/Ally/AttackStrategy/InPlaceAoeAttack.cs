using UnityEngine;

public class InPlaceAoeAttack : RangedAoeAttack{


    public InPlaceAoeAttack(float radius=5f) : base(radius){
        
    }

    protected override bool IsInAreaOfEffect(EnemyHealth e){
        return IsInRadius(e.transform.position);
    }

    public override void PlayAnimation(){
        //tower.GetComponent<Animator>().Play("Default Take");
        base.PlayAnimation(); //temp
    }

    protected override void DisplayAoeRadius(){
        var zoneTemplate = GameObject.Find("AttackZone");
        var attackZone = GameObject.Instantiate(zoneTemplate, tower.transform.position+ new Vector3(0,1,0), zoneTemplate.transform.rotation);
        float r = 2*GetAoeRadius();
        attackZone.GetComponentInChildren<UnityEngine.UI.Image>().transform.localScale = new Vector3(r,r,r);
        GameObject.Destroy(attackZone, 0.5f);
    }

    protected override float GetAoeRadius(){
        return tower.radius/15;
    }

}