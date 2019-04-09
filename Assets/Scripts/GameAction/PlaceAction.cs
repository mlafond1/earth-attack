using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceAction : GameAction
{

    GameObject nextStructure;
    MapHelper2 mapHelper;

    public PlaceAction(GameObject nextStructure){
        this.nextStructure = nextStructure;
        mapHelper = MapHelper2.GetInstance();
    }

    override public void Execute(){
        if(Input.GetMouseButtonDown(0)){
            PlaceStructure();
        }
        // Cancel Action
        if(Input.GetKeyDown(KeyCode.Q)){
            CancelAction();
        }
    }

    public void PlaceStructure(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) return;

        Debug.Log("Collided with " + hit.collider.name + " at " + hit.point);
        // Find index of Tile
        Vector2Int indexes = mapHelper.GetIndexesFromCoordinate(hit.point);
        if (!mapHelper.IndexesInBound(indexes)) return;
        // Find new position with index
        Vector3 pos = mapHelper.GetCoordinateFromIndexes(indexes);
        pos.y = hit.point.y;
        bool[,] tileMap = mapHelper.tiles;
        if(tileMap[indexes.x, indexes.y]){ // Tile Occupied
            Debug.Log("Tile already occupied");
        }
        else { // Tile Open
            GameObject clone = GameObject.Instantiate(nextStructure, pos, nextStructure.transform.rotation);
            clone.name = nextStructure.name + "" + indexes;
            Debug.Log("Object is at index " + indexes);
            Debug.Log("Object now at " + nextStructure.transform.position);
            tileMap[indexes.x, indexes.y] = true;
            // Pourrait occasionner un coût en ressource (in game)
            // Remove Selection After usage
            // nextStructure = null;
        }
        
    }

}
