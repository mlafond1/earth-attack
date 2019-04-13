using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAction : GameAction
{
    MapHelper2 mapHelper;

    public DestroyAction(){
        mapHelper = MapHelper2.GetInstance();
    }

    override public void Execute(){
        if(Input.GetMouseButtonDown(0)){
            DestroyStructure();
        }
        // Cancel Action
        if(Input.GetKeyDown(KeyCode.Q)){
            CancelAction();
        }
    }

    public void DestroyStructure(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) return;

        //Debug.Log("Collided with " + hit.collider.name + " at " + hit.point);
        // Find index of Tile
        Vector2Int indexes = mapHelper.GetIndexesFromCoordinate(hit.point);
        if (!mapHelper.IndexesInBound(indexes)) return;
        
        bool[,] tileMap = mapHelper.tiles;
        if(tileMap[indexes.x, indexes.y] && hit.transform.gameObject.GetComponent<AttackEnemy>() != null){ // Tile Occupied
            GameObject.Destroy(hit.transform.gameObject);
            //Debug.Log("Object destroyed at " + indexes);
            tileMap[indexes.x, indexes.y] = false;
        }
        else { // Tile Open
            //Debug.Log("No object to destroy here");
        }
    }

    

}
