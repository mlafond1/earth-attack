using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoints
{
    public static Vector3[] points {get; private set;}
    public static bool isReady {get; private set;} = false;

    private FollowPoints(){

    }

    public static void GeneratePathFromIndexes(Vector2Int[] path, MapHelper2 mapHelper){
        isReady = false;
        points = new Vector3[path.Length];
        for(int i = 0; i < path.Length; ++i){
            points[i] = mapHelper.GetCoordinateFromIndexes(path[i]);
        }
        isReady = true;
    }

    public static void GeneratePathFromCoordinates(Vector3[] path){
        points = path;
        isReady = true;
    }

}
