using System;
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
        LoadData.JsonDefenses.JsonTower towerJson = towers.Find((t) => t.name.Equals(name));
        if(towerJson == null) return null;
        if(upgradeIndex >= towerJson.level.Count) return null;
        LoadData.JsonDefenses.JsonTower.JsonTowerLevel level = towerJson.level[upgradeIndex];
        // SetTexture
        GameObject prefab = Resources.Load<GameObject>("Prefabs/"+textureToModel[towerJson.name]);
        if(prefab == null) return null;
        // SetStats
        Tower tower = prefab.GetComponent<Tower>();
        tower.towerName = towerJson.name;
        tower.description = towerJson.description;
        tower.cost = level.cost;
        tower.radius = level.radius;
        tower.power = level.power;
        tower.attackSpeed = level.attackSpeed;
        tower.upgradeIndex = upgradeIndex +1;
        tower.targetedAttributes.Clear();
        foreach(var target in towerJson.target){
            EnemyAttribute att = EnemyAttribute.NONE;
            if(System.Enum.TryParse(target.ToUpper(), out att)){
                tower.targetedAttributes.Add(att);
            }
        }
        // SetProjectile (Should already be there by default in the prefab)
        GameObject projectile = Resources.Load<GameObject>("Prefabs/"+textureToModel[level.projectile]);
        tower.projectile = projectile;
        tower.SetAttackStrategy(LoadAttackStrategy(tower.towerName));
        return prefab;
    } 

    public GameObject Build(string name){
        Tower ae = lastBuilt?.GetComponent<Tower>();
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
        Tower tower = prefab.GetComponent<Tower>();
        tower.towerName = "scout";
        tower.cost = 10f;
        tower.radius = 10f;
        tower.power = 10f;
        tower.attackSpeed = 250f;
        tower.upgradeIndex = 1;
        tower.targetedAttributes.Clear();
        tower.targetedAttributes.Add(EnemyAttribute.GROUND);
        tower.targetedAttributes.Add(EnemyAttribute.AIR);
        tower.SetAttackStrategy(LoadAttackStrategy(tower.towerName));
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
        textureToModel["bullet"] = "projectile_0";
        textureToModel["laser"] = "projectile_0";
        textureToModel["petrolBall"] = "projectile_0";
        textureToModel["missile"] = "projectile_0";
        textureToModel["arcWave"] = "projectile_0";
        textureToModel["bomb"] = "projectile_0";
        textureToModel["bigMissile"] = "projectile_0";
    }

    public AttackStrategy LoadAttackStrategy(string towerName) {
        AttackStrategy strategy = null;
        switch(towerName){
            case "scout":
                strategy = new NormalAttack();
                break;
            case "laserTower":
                strategy = new NormalAttack();
                break;
            case "petrolGun":
                strategy = new RangedAoeSlowAttack();
                break;
            case "antiAir":
                strategy = new NormalAttack();
                break;
            case "hammer":
                strategy = new InPlaceAoeAttack();
                break;
            case "bombTower":
                strategy = new RangedAoeAttack();
                break;
            case "nuke":
                strategy = new RandomRangedAoeAttack();
                break;
        }
        return strategy;
    }

}