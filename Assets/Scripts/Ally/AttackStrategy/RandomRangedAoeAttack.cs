using UnityEngine;

public class RandomRangedAoeAttack : RangedAoeAttack{

    private System.Random r;
    private Vector3 targetLocation;
    private MapHelper2 mapHelper;

    bool hasFocus;

    public RandomRangedAoeAttack(float radius=20f) : base(radius){
        r = new System.Random();
        mapHelper = MapHelper2.GetInstance();
        hasFocus = false;
    }

    public override void AcquireTarget(){
        base.AcquireTarget();
        if(focus == null) return;
        int x = r.Next(0,mapHelper.nbTiles);
        int y = r.Next(0,mapHelper.nbTiles);
        targetLocation = mapHelper.GetCoordinateFromIndexes(new Vector2Int(x,y));
        if(tower.name == tower.towerName + new Vector2Int(x,y)) {
            AcquireTarget();
            return;
        }
        hasFocus = true;
    }

    public override bool IsFocusInRadius(){
        return hasFocus;
    }

    public override bool HasFocus(){
        return hasFocus;
    }

    public override void LoseFocus(){
        hasFocus = false;
    }

    public override Vector3 GetTargetLocation(){
        return targetLocation;
    }

    public override void Attack(){
        GameObject gameProjectile = GameObject.Instantiate(tower.projectile, tower.transform.position + new Vector3(0,1,0), tower.transform.rotation);
        gameProjectile.GetComponent<ProjectileAnimation>().SetTarget(targetLocation, 15f);
        EnemyHealth[] enemies = GameObject.FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemy in enemies){
            if(!tower.targetedAttributes.Contains(enemy.attribute)) continue;
            if(IsInAreaOfEffect(enemy)){
                enemy.TakeDamage(tower.power);
                SpecialEffect(enemy);
            }
        }
        DisplayAoeRadius();
        LoseFocus();
    }

    protected override bool IsInAreaOfEffect(EnemyHealth e){
        float distance = Vector3.Distance(targetLocation, e.transform.position);
        return distance <= aoeRadius;
    }

}