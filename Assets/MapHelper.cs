using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHelper {

    Collider mapCollider;
    float tileDimension_x = 0;
    float tileDimension_z = 0;
    int nbTiles = 0;

    public MapHelper(GameObject map,  int nbTiles){
        if(map.GetComponent<Collider>() == null) 
            map.AddComponent<BoxCollider>();
        mapCollider = map.GetComponent<Collider>();
        this.nbTiles = nbTiles;
        tileDimension_x = mapCollider.bounds.size.x/nbTiles;
        tileDimension_z = mapCollider.bounds.size.z/nbTiles;
    }

    public Vector2Int getIndexesFromCoordinate(Vector3 coordinates){
        int index_x = Mathf.FloorToInt((coordinates.x + mapCollider.bounds.size.x/2f)/tileDimension_x);
        int index_z = Mathf.FloorToInt((coordinates.z + mapCollider.bounds.size.z/2f)/tileDimension_z);
        return new Vector2Int(index_x, index_z);
    }

    public Vector3 getCoordinateFromIndexes(Vector2Int indexes){
        float pos_x = (indexes.x * tileDimension_x) + tileDimension_x/2f - mapCollider.bounds.size.x/2f;
        float pos_z = (indexes.y * tileDimension_z) + tileDimension_z/2f - mapCollider.bounds.size.z/2f;
        return new Vector3(pos_x, mapCollider.bounds.size.y/2f ,pos_z);
    }

}