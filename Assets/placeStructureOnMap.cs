using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceStructureOnMap : MonoBehaviour
{
    private static int DEFAULT_DIMENSION = 11;
    private static GameObject nextStructure;
    int dimensionTileMap;
    bool[,] tileMap;
    float tileDimension_x;
    float tileDimension_z;

    // Start is called before the first frame update
    void Start()
    {
        //nextStructure = GameObject.Find("constructionSol_1");
        bool[,] defaultTiles = new bool[DEFAULT_DIMENSION, DEFAULT_DIMENSION];
        SetUpMap(defaultTiles, DEFAULT_DIMENSION);
    }

    public static void ChangeStructure(GameObject structure){
        nextStructure = structure;
    }

    public void SetUpMap(bool[,] tiles, int length){
        dimensionTileMap = length;
        tileMap = tiles;
        if(GetComponent<Collider>() == null) 
            gameObject.AddComponent<BoxCollider>();
        tileDimension_x = GetComponent<Collider>().bounds.size.x/dimensionTileMap;
        tileDimension_z = GetComponent<Collider>().bounds.size.z/dimensionTileMap;
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
                int index_x = Mathf.FloorToInt((hit.point.x + hit.collider.bounds.size.x/2f)/tileDimension_x);
                int index_z = Mathf.FloorToInt((hit.point.z + hit.collider.bounds.size.x/2f)/tileDimension_z);
                // Find new position with index
                float pos_x = (index_x * tileDimension_x) + tileDimension_x/2f - hit.collider.bounds.size.x/2f;
                float pos_z = (index_z * tileDimension_z) + tileDimension_z/2f - hit.collider.bounds.size.z/2f;

                if(tileMap[index_x, index_z]){ // Tile Occupied
                    Debug.Log("Tile already occupied");
                }
                else { // Tile Open
                    GameObject clone = Instantiate(nextStructure, new Vector3(pos_x, hit.point.y, pos_z), nextStructure.transform.rotation);
                    clone.name = nextStructure.name + "("+index_x+"," +index_z+")";
                    Debug.Log("Object is at index " + new Vector2(index_x, index_z));
                    Debug.Log("Object now at " + nextStructure.transform.position);
                    tileMap[index_x, index_z] = true;
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
