using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceStructureOnMap : MonoBehaviour
{
    private static int DEFAULT_DIMENSION = 11;
    private static GameObject nextStructure;
    int nbTiles = 1;
    bool[,] tileMap;

    MapHelper mapHelper;

    // Start is called before the first frame update
    void Start()
    {
        //bool[,] defaultTiles = new bool[DEFAULT_DIMENSION, DEFAULT_DIMENSION];
        //SetUpMap(defaultTiles, DEFAULT_DIMENSION);
        SetUpMapFromFile("map1.txt");
        Debug.Log("Map size: " + nbTiles+"x"+nbTiles);
    }
    
    public void SetUpMapFromFile(string fileName){
        MapHelper.loadFromTxtFile(fileName, out tileMap, out nbTiles);
        mapHelper = new MapHelper(this.gameObject, nbTiles);
    }

    public void SetUpMap(bool[,] tiles, int length){
        nbTiles = length;
        tileMap = tiles;
        mapHelper = new MapHelper(this.gameObject, nbTiles);
    }

    public static void ChangeStructure(GameObject structure){
        nextStructure = structure;
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0) && nextStructure != null){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
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
        }
        // Cancel Selection
        if(Input.GetKeyDown(KeyCode.Q)){
            nextStructure = null;
        }
    }
}
