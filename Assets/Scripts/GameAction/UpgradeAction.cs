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

        Debug.Log("Collided with " + hit.collider.name + " at " + hit.point);
        // Find index of Tile
        Vector2Int indexes = mapHelper.GetIndexesFromCoordinate(hit.point);
        
        bool[,] tileMap = mapHelper.tiles;
        AttackEnemy ae = hit.transform.gameObject.GetComponent<AttackEnemy>();
        bool canUpgrade = true; // Change
        if(tileMap[indexes.x, indexes.y] && ae != null && canUpgrade){ // Tile Occupied
            Debug.Log("Upgrading at " + indexes);
            // Do something
        }
        else { // Tile Open
            Debug.Log("No object to upgrade here");
        }
    }

}
