using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceStructureOnMap : MonoBehaviour
{
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
        Waypoints.GeneratePathFromIndexes(path, mapHelper);
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
