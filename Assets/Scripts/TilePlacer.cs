using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlacer : MonoBehaviour
{
    
    private MapHelper2 mapHelper;
    public Transform tileModel;

    void Start()
    {
        SetUpMapFromFile("map1.txt");
        Debug.Log("Map size: " + mapHelper.nbTiles+"x"+mapHelper.nbTiles);
    }
    
    public void SetUpMapFromFile(string fileName){
        MapHelper2.LoadFromTxtFile(fileName);
        mapHelper = MapHelper2.GetInstance();
        mapHelper.ChangeMap(GameObject.Find("flat_map")); // temp
        //.ChangeMap(GameObject.FindGameObjectWithTag("map"));
        PlaceTilesOnMap(mapHelper.path);
        FollowPoints.GeneratePathFromIndexes(mapHelper.path, mapHelper);
    }

    public void PlaceTilesOnMap(Vector2Int[] path){
        Quaternion orientation = Quaternion.identity;
        for (int i = 0; i < path.Length-1; ++i){
            Vector2Int currentIndexes = path[i];
            Vector2Int nextIndexes = path[i+1];
            Vector3 currentPosition = mapHelper.GetCoordinateFromIndexes(currentIndexes);
            Vector2Int nextDirection = calculateNextDirection(currentIndexes, nextIndexes, out orientation);
            while(!currentIndexes.Equals(nextIndexes)){                
                Transform tile = Instantiate(tileModel, mapHelper.GetCoordinateFromIndexes(currentIndexes), orientation);
                tile.localScale = new Vector3(mapHelper.tileDimension_x/2, mapHelper.tileDimension_z/2, 0.25f);
                currentIndexes += nextDirection;
            }
        }
    }

    private Vector2Int calculateNextDirection(Vector2Int currentPosition, Vector2Int nextPosition, out Quaternion orientation){
        Vector2Int res = Vector2Int.zero;
        Vector2Int diff = currentPosition - nextPosition;
        orientation = tileModel.rotation;
        if(diff.x != 0){
            if(diff.x > 0){
                res = Vector2Int.left;
            }
            else {
                res = Vector2Int.right;
            }
            tileModel.Rotate(0,0,90f);
            orientation = tileModel.rotation;
            tileModel.Rotate(0,0,-90f);
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
}
