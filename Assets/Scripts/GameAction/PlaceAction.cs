using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceAction : GameAction
{

    string towerName;
    MapHelper2 mapHelper;
    TowerFactory factory;

    public PlaceAction(string towerName){
        mapHelper = MapHelper2.GetInstance();
        factory = TowerFactory.GetInstance();
        this.towerName = towerName;
        if(factory.Build(towerName) == null) CancelAction();
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

        //Debug.Log("Collided with " + hit.collider.name + " at " + hit.point);
        // Find index of Tile
        Vector2Int indexes = mapHelper.GetIndexesFromCoordinate(hit.point);
        if (!mapHelper.IndexesInBound(indexes)) return;
        // Find new position with index
        Vector3 pos = mapHelper.GetCoordinateFromIndexes(indexes);
        pos.y = hit.point.y;
        bool[,] tileMap = mapHelper.tiles;
        if(tileMap[indexes.x, indexes.y]){ // Tile Occupied
            //Debug.Log("Tile already occupied");
        }
        else { // Tile Open
            RessourceManager ressourceManager = RessourceManager.GetInstance();
            AttackEnemy ae = factory.Build(towerName).GetComponent<AttackEnemy>();
            if(!ressourceManager.Spend(ae.cost)) return;
            GameObject clone = GameObject.Instantiate(ae.gameObject, pos, ae.transform.rotation);
            clone.name = ae.towerName + "" + indexes;
            tileMap[indexes.x, indexes.y] = true;
        }
        
    }

}
