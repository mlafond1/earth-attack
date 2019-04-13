using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory {

    private List<LoadData.JsonWaves.JsonEnemy> enemies;
    private static Dictionary<string, string> textureToModel;
    
    public EnemyFactory(List<LoadData.JsonWaves.JsonEnemy> enemies){
        this.enemies = enemies;
        if(textureToModel == null) LoadTextureToModel();
    }

    public GameObject Build(string name){
        LoadData.JsonWaves.JsonEnemy enemyJson = enemies.Find((e) => e.name.Equals(name));
        if(enemyJson == null) return null;
        // SetTexture
        GameObject prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/"+textureToModel[enemyJson.texture]+".prefab");
        if(prefab == null) return null;
        // SetHp
        EnemyHealth health = prefab.GetComponent<EnemyHealth>();
        health.SetMaxHealth(enemyJson.hp);
        // SetSpeed
        EnemyMovement movement = prefab.GetComponent<EnemyMovement>();
        movement.speed = enemyJson.speed;
        // TODO SetValue
        return prefab;
    } 

    // In case of problems
    public GameObject BuildDefault(string name){
        LoadData.JsonWaves.JsonEnemy enemyJson = enemies.Find((e) => e.name.Equals(name));
        if(enemyJson == null) return null;
        // SetTexture
        GameObject prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/"+textureToModel[enemyJson.texture]+".prefab");
        if(prefab == null) return null;
        // SetHp
        EnemyHealth health = prefab.GetComponent<EnemyHealth>();
        health.SetMaxHealth(100f);
        // SetSpeed
        EnemyMovement movement = prefab.GetComponent<EnemyMovement>();
        movement.speed = 10f;
        return prefab;
    }

    private static void LoadTextureToModel(){
        textureToModel = new Dictionary<string, string>();
        textureToModel["astronaut"] = "ennemy_astronaut";
        textureToModel["tank"] = "ennemy_tank";
        textureToModel["jet"] = "ennemy_jet";
    }

}