using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    private List<LoadData.JsonDefenses.JsonTower> towers;
    private static Dictionary<string, string> nameToModel;
    private static TowerFactory instance;

    private GameObject lastBuilt;
    
    void Start(){
        this.towers = LoadData.LoadTowers("Json/defenses").towers;
        if(nameToModel == null) LoadNameToModel();
        instance = this;
    }

    public GameObject Build(string name, int upgradeIndex){
        LoadData.JsonDefenses.JsonTower towerJson = towers.Find((t) => t.name.Equals(name));
        if(towerJson == null) return null;
        if(upgradeIndex >= towerJson.level.Count) return null;
        LoadData.JsonDefenses.JsonTower.JsonTowerLevel level = towerJson.level[upgradeIndex];
        // SetTexture
        GameObject prefab = Resources.Load<GameObject>("Prefabs/"+nameToModel[towerJson.name]);
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
        GameObject projectile = Resources.Load<GameObject>("Prefabs/"+nameToModel[level.projectile]);
        tower.projectile = projectile;
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
        GameObject prefab = Resources.Load<GameObject>("Prefabs/"+nameToModel[towerJson.name]);
        if(prefab == null) return null;
        Tower tower = prefab.GetComponent<Tower>();
        // SetStats
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

    private static void LoadNameToModel(){
        nameToModel = new Dictionary<string, string>();
        // Towers
        nameToModel["scout"] = "tower_scout";
        nameToModel["laserTower"] = "tower_laser";
        nameToModel["petrolGun"] = "tower_petrol";
        nameToModel["antiAir"] = "tower_antiair";
        nameToModel["hammer"] = "tower_hammer";
        nameToModel["bombTower"] = "constructionSol_1";
        nameToModel["nuke"] = "constructionSol_1";
        // Projectiles
        nameToModel["bullet"] = "projectile_1";
        nameToModel["laser"] = "projectile_laser";
        nameToModel["petrolBall"] = "projectile_petrol";
        nameToModel["missile"] = "projectile_1";
        nameToModel["arcWave"] = "projectile_0";
        nameToModel["bomb"] = "projectile_1";
        nameToModel["bigMissile"] = "projectile_petrol";
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

    public void LoadTexture(Tower tower){
        Renderer renderer = tower.gameObject.GetComponent<Renderer>();
        if(renderer == null){
            renderer = tower.gameObject.GetComponentInChildren<Renderer>();
        }
        if(tower.upgradeIndex-1 < tower.textures.Length && tower.upgradeIndex-1 >=0)
            renderer.material.SetTexture("_MainTex", tower.textures[tower.upgradeIndex-1]);
    }

}