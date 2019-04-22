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
        Tower tower = hit.transform.gameObject.GetComponent<Tower>();
        if(tileMap[indexes.x, indexes.y] && tower != null){ // Tile Occupied
            TowerFactory factory = TowerFactory.GetInstance();
            GameObject upgradedTower = factory.Build(tower.towerName, tower.upgradeIndex);
            if(upgradedTower == null) return;
            //Debug.Log("Upgrading at " + indexes);
            // Upgrade
            RessourceManager ressourceManager = RessourceManager.GetInstance();
            Tower uae = upgradedTower.GetComponent<Tower>();
            if(!ressourceManager.Spend(uae.cost)) return;
            Vector3 position = tower.transform.position;
            Quaternion rotation = tower.transform.rotation;
            GameObject.Destroy(tower.gameObject);
            GameObject.Instantiate(upgradedTower, position, rotation).name = tower.towerName + "" + indexes;
        }
        else { // Tile Open
            //Debug.Log("No object to upgrade here");
        }
    }

}
