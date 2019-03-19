using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Vector3[] points;
    public static bool isReady = false;

    void Start()
    {
        points = new Vector3[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i).position;
        }
        isReady = true;
    }

    public static void GeneratePathFromIndexes(Vector2Int[] path, MapHelper mapHelper){
        isReady = false;
        points = new Vector3[path.Length];
        for(int i = 0; i < path.Length; ++i){
            points[i] = mapHelper.getCoordinateFromIndexes(path[i]);
        }
        isReady = true;
    }

    public static void GeneratePathFromCoordinates(Vector3[] path){
        points = path;
        isReady = true;
    }

}
