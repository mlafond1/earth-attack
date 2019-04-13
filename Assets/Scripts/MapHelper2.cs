using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHelper2 {

    private static MapHelper2 instance;
    private Collider mapCollider;
    public float tileDimension_x {get; private set;} = 0;
    public float tileDimension_z {get; private set;} = 0;
    public int nbTiles {get; private set;} = 0;
    public bool[,] tiles {get; private set;}
    public Vector2Int[] path {get; private set;}

    public int[] rotations {get; private set;} // 0->aucune; 90->droite; -90->gauche

    private MapHelper2(){

    }

    public Vector2Int GetIndexesFromCoordinate(Vector3 coordinates){
        //int index_x = Mathf.FloorToInt((coordinates.x + mapCollider.bounds.size.x/2f)/tileDimension_x);
        //int index_z = Mathf.FloorToInt((coordinates.z + mapCollider.bounds.size.z/2f)/tileDimension_z);
        int index_x = Mathf.FloorToInt((coordinates.x)/tileDimension_x);
        int index_z = Mathf.FloorToInt((coordinates.z)/tileDimension_z);
        return new Vector2Int(index_x, index_z);
    }

    public Vector3 GetCoordinateFromIndexes(Vector2Int indexes){
        //float pos_x = (indexes.x * tileDimension_x) + tileDimension_x/2f - mapCollider.bounds.size.x/2f;
        //float pos_z = (indexes.y * tileDimension_z) + tileDimension_z/2f - mapCollider.bounds.size.z/2f;
        float pos_x = (indexes.x * tileDimension_x) + tileDimension_x/2f;// - mapCollider.bounds.size.x/2f;
        float pos_z = (indexes.y * tileDimension_z) + tileDimension_z/2f;// - mapCollider.bounds.size.z/2f;
        return new Vector3(pos_x, mapCollider.bounds.size.y/2f ,pos_z);
    }

    public bool IndexesInBound(Vector2Int indexes){
        bool inBound = true;
        if (indexes.x >= nbTiles || indexes.x < 0 || indexes.y >= nbTiles || indexes.y < 0) 
            inBound = false;
        return inBound;
    }

    public static void LoadFromTxtFile(string fileName){
        instance = GetInstance();
        string[] lines = System.IO.File.ReadAllLines(@fileName);
        int nb = 0;
        bool success = int.TryParse(lines[0], out nb) && 
                       LoadPathFromString(lines[1]) && 
                       LoadRotations(lines[1]); 
        instance.nbTiles = nb;
        if (!success) return;
        instance.tiles = new bool[instance.nbTiles,instance.nbTiles];
        for(int line = 2; line < lines.Length; ++line){
            for(int col = 0; col < lines[line].Length; ++col){
                char c = lines[line][col];
                if(c != ' '){
                    instance.tiles[line-2, col] = true;
                }
            }
        }
    }

    public static bool LoadPathFromString(string linePath){
        instance = GetInstance();
        string[] strPath = linePath.Split(' ');
        instance.path = new Vector2Int[strPath.Length];
        bool success = false;
        for(int i = 0; i < strPath.Length; ++i){
            string[] indexes = strPath[i].Split(';');
            int x = 0, y = 0;
            success = int.TryParse(indexes[0], out x) && int.TryParse(indexes[1], out y);
            if(!success) break;
            instance.path[i] = new Vector2Int(x, y);
            //Debug.Log( i + " : " + instance.path[i]);
        }
        return success;
    }

    public static bool LoadRotations(string linePath){
        //Debug.Log("Rotations\n-----");
        instance = GetInstance();
        bool success = true;
        if(instance.path.Length == 0) success = LoadPathFromString(linePath);
        instance.rotations = new int[instance.path.Length-1];
        if(!success) return false;
        int prevOrientation = -1;
        int nextOrientation = -1;
        for(int i = 0; i < instance.path.Length-1; ++i){
            //Find Rotation from current
            Vector2Int currentIndexes = instance.path[i];
            Vector2Int nextIndexes = instance.path[i+1];
            nextOrientation = calculateNextOrientation(currentIndexes, nextIndexes);
            if(prevOrientation == -1 || nextOrientation == prevOrientation)
                instance.rotations[i] = 0;
            else if((prevOrientation+1)%4 == nextOrientation)
                instance.rotations[i] = 90;
            else
                instance.rotations[i] = -90;
            prevOrientation = nextOrientation;
            //Debug.Log("Rot " + i + ": " + instance.rotations[i]);
        }
        //Debug.Log("-----");
        return success;
    }

    private static int calculateNextOrientation(Vector2Int currentPosition, Vector2Int nextPosition){
        int res = 0;
        Vector2Int diff = currentPosition - nextPosition;
        if(diff.x != 0){
            if(diff.x > 0){
                res = 3; // Left
            }
            else {
                res = 1; // Right
            }
        }
        else if(diff.y != 0) {
            if(diff.y > 0){
                res = 2; // Down
            }
            else {
                res = 0; // Up
            }
        }
        return res;
    }

    public void ChangeMap(GameObject map){
        if(map.GetComponent<Collider>() == null) 
            map.AddComponent<BoxCollider>();
        mapCollider = map.GetComponent<Collider>();
        tileDimension_x = mapCollider.bounds.size.x/nbTiles;
        tileDimension_z = mapCollider.bounds.size.z/nbTiles;
    }

    public static MapHelper2 GetInstance(){
        if(instance == null) 
            instance = new MapHelper2();
        return instance;
    }

}