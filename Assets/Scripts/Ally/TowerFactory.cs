using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    private List<LoadData.JsonDefenses.JsonTower> towers;
    private static Dictionary<string, string> textureToModel;
    private static TowerFactory instance;

    private GameObject lastBuilt;
    
    void Start(){
        this.towers = LoadData.LoadTowers("Json/defenses").towers;
        if(textureToModel == null) LoadTextureToModel();
        instance = this;
    }

    public GameObject Build(string name, int upgradeIndex){
        LoadData.JsonDefenses.JsonTower towerJson = towers.Find((e) => e.name.Equals(name));
        if(towerJson == null) return null;
        if(upgradeIndex >= towerJson.level.Count) return null;
        LoadData.JsonDefenses.JsonTower.JsonTowerLevel level = towerJson.level[upgradeIndex];
        // SetTexture
        GameObject prefab = Resources.Load<GameObject>("Prefabs/"+textureToModel[towerJson.name]);
        if(prefab == null) return null;
        // SetStats
        AttackEnemy attack = prefab.GetComponent<AttackEnemy>();
        attack.towerName = towerJson.name;
        attack.description = towerJson.description;
        attack.cost = level.cost;
        attack.radius = level.radius;
        attack.power = level.power;
        attack.attackSpeed = level.attackSpeed;
        attack.upgradeIndex = upgradeIndex +1;
        attack.targetedAttributes.Clear();
        foreach(var t in towerJson.target){
            EnemyAttribute att = EnemyAttribute.NONE;
            if(System.Enum.TryParse(t.ToUpper(), out att)){
                attack.targetedAttributes.Add(att);
            }
        }
        // SetProjectile (Should already be there by default in the prefab)
        GameObject projectile = Resources.Load<GameObject>("Prefabs/"+textureToModel[level.projectile]);
        attack.projectile = projectile;
        return prefab;
    } 

    public GameObject Build(string name){
        AttackEnemy ae = lastBuilt?.GetComponent<AttackEnemy>();
        if(lastBuilt == null || ae.towerName != name || ae.upgradeIndex != 0)
            lastBuilt = Build(name, 0);
        return lastBuilt;
    } 

    // In case of problems
    public GameObject BuildDefault(string name){
        LoadData.JsonDefenses.JsonTower towerJson = towers.Find((e) => e.name.Equals(name));
        if(towerJson == null) return null;
        LoadData.JsonDefenses.JsonTower.JsonTowerLevel level = towerJson.level[0];
        // SetTexture
        GameObject prefab = Resources.Load<GameObject>("Prefabs/"+textureToModel[towerJson.name]);
        if(prefab == null) return null;
        AttackEnemy attack = prefab.GetComponent<AttackEnemy>();
        attack.towerName = "scout";
        attack.cost = 10f;
        attack.radius = 10f;
        attack.power = 10f;
        attack.attackSpeed = 250f;
        attack.upgradeIndex = 1;
        attack.targetedAttributes.Clear();
        attack.targetedAttributes.Add(EnemyAttribute.GROUND);
        attack.targetedAttributes.Add(EnemyAttribute.AIR);
        return prefab;
    }

    public static TowerFactory GetInstance(){
        return instance;
    }

    private static void LoadTextureToModel(){
        textureToModel = new Dictionary<string, string>();
        // Towers
        textureToModel["scout"] = "constructionSol_1";
        textureToModel["laserTower"] = "constructionSol_1";
        textureToModel["petrolGun"] = "constructionSol_1";
        textureToModel["antiAir"] = "constructionSol_1";
        textureToModel["hammer"] = "constructionSol_1";
        textureToModel["bombTower"] = "constructionSol_1";
        textureToModel["nuke"] = "constructionSol_1";
        // Projectiles
        textureToModel["bullet"] = "projectile_1";
        textureToModel["laser"] = "projectile_1";
        textureToModel["petrolBall"] = "projectile_1";
        textureToModel["missile"] = "projectile_1";
        textureToModel["arcWave"] = "projectile_1";
        textureToModel["bomb"] = "projectile_1";
        textureToModel["bigMissile"] = "projectile_1";
    }

}