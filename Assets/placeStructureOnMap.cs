using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeStructureOnMap : MonoBehaviour
{

    GameObject nextStructure;
    int dimensionTileMap = 11;
    float tileDimension_x = 0;
    float tileDimension_z = 0;

    // Start is called before the first frame update
    void Start()
    {
        nextStructure = GameObject.Find("constructionSol_1");
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

                nextStructure.transform.position = new Vector3(pos_x, hit.point.y, pos_z);
                
                Debug.Log("Object is at index " + new Vector2(index_x, index_z));
                Debug.Log("Object now at " + nextStructure.transform.position);
                
                // nextStructure = null;
            }
        }
    }
}
