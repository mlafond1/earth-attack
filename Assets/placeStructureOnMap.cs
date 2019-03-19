using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceStructureOnMap : MonoBehaviour
{
    public Transform tileModel;
    private static GameObject nextStructure;
    int nbTiles = 1;
    bool[,] tileMap;

    MapHelper mapHelper;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMapFromFile("map1.txt");

        Debug.Log("Map size: " + nbTiles+"x"+nbTiles);
    }
    
    public void SetUpMapFromFile(string fileName){
        Vector2Int[] path;
        MapHelper.loadFromTxtFile(fileName, out tileMap, out nbTiles, out path);
        mapHelper = new MapHelper(this.gameObject, nbTiles);
        //PlaceTilesOnMap(path);
        Waypoints.GeneratePathFromIndexes(path, mapHelper);
    }

    public void PlaceTilesOnMap(Vector2Int[] path){
        Quaternion turn90d = tileModel.rotation;
        for (int i = 0; i < path.Length-1; ++i){
            Vector2Int currentIndexes = path[i];
            Vector2Int nextIndexes = path[i+1];
            Vector3 currentPosition = mapHelper.getCoordinateFromIndexes(currentIndexes);
            Vector2Int nextDirection = calculateNextDirection(currentIndexes, nextIndexes);
            while(!currentIndexes.Equals(nextIndexes)){                
                Transform tile = Instantiate(tileModel, mapHelper.getCoordinateFromIndexes(path[i]), tileModel.rotation);
                tile.localScale = new Vector3(mapHelper.getTileDimension()/2, mapHelper.getTileDimension()/2, 0.25f);
                currentIndexes += nextDirection;
            }
        }
    }

    private Vector2Int calculateNextDirection(Vector2Int currentPosition, Vector2Int nextPosition){
        Vector2Int res = Vector2Int.zero;
        Vector2Int diff = currentPosition - nextPosition;
        if(diff.x != 0){
            if(diff.x > 0){
                res = Vector2Int.left;
            }
            else {
                res = Vector2Int.right;
            }
        }
        else if(diff.y != 0) {
            if(diff.y > 0){
                res = Vector2Int.down;
            }
            else {
                res = Vector2Int.up;
            }
        }
        return res;
    }

    public static void ChangeStructure(GameObject structure){
        nextStructure = structure;
    }

    public void PlaceStructure(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) return;

        Debug.Log("Collided with " + hit.collider.name + " at " + hit.point);
        // Find index of Tile
        Vector2Int indexes = mapHelper.getIndexesFromCoordinate(hit.point);
        // Find new position with index
        Vector3 pos = mapHelper.getCoordinateFromIndexes(indexes);
        pos.y = hit.point.y;
        
        if(tileMap[indexes.x, indexes.y]){ // Tile Occupied
            Debug.Log("Tile already occupied");
        }
        else { // Tile Open
            GameObject clone = Instantiate(nextStructure, pos, nextStructure.transform.rotation);
            clone.name = nextStructure.name + "" + indexes;
            Debug.Log("Object is at index " + indexes);
            Debug.Log("Object now at " + nextStructure.transform.position);
            tileMap[indexes.x, indexes.y] = true;
            // Pourrait occasionner un coût en ressource (in game)
            // Remove Selection After usage
            // nextStructure = null;
        }
        
    }

    void CancelSelection(){
        nextStructure = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0) && nextStructure != null){
            PlaceStructure();
        }
        // Cancel Selection
        if(Input.GetKeyDown(KeyCode.Q)){
            CancelSelection();
        }
    }
}
