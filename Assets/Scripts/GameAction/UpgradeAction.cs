using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAction : GameAction
{
    MapHelper2 mapHelper;

    public UpgradeAction(){
        mapHelper = MapHelper2.GetInstance();
    }

    override public void Execute(){
        if(Input.GetMouseButtonDown(0)){
            UpgradeStructure();
        }
        // Cancel Action
        if(Input.GetKeyDown(KeyCode.Q)){
            CancelAction();
        }
    }

    public void UpgradeStructure(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) return;

        //Debug.Log("Collided with " + hit.collider.name + " at " + hit.point);
        // Find index of Tile
        Vector2Int indexes = mapHelper.GetIndexesFromCoordinate(hit.point);
        if (!mapHelper.IndexesInBound(indexes)) return;
        
        bool[,] tileMap = mapHelper.tiles;
        AttackEnemy ae = hit.transform.gameObject.GetComponent<AttackEnemy>();
        if(tileMap[indexes.x, indexes.y] && ae != null){ // Tile Occupied
            TowerFactory factory = TowerFactory.GetInstance();
            GameObject upgradedTower = factory.Build(ae.towerName, ae.upgradeIndex);
            if(upgradedTower == null) return;
            //Debug.Log("Upgrading at " + indexes);
            // Upgrade
            RessourceManager ressourceManager = RessourceManager.GetInstance();
            AttackEnemy uae = upgradedTower.GetComponent<AttackEnemy>();
            if(!ressourceManager.Spend(uae.cost)) return;
            Vector3 position = ae.transform.position;
            Quaternion rotation = ae.transform.rotation;
            GameObject.Destroy(ae.gameObject);
            GameObject.Instantiate(upgradedTower, position, rotation);
        }
        else { // Tile Open
            //Debug.Log("No object to upgrade here");
        }
    }

}
