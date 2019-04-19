using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData
{
    
    public static JsonWaves LoadWaves(string filename){
        return JsonUtility.FromJson<JsonWaves>(Resources.Load<TextAsset>(@filename).text);
    }

    public static JsonDefenses LoadTowers(string filename){
        return JsonUtility.FromJson<JsonDefenses>(Resources.Load<TextAsset>(@filename).text);
    }

    [System.Serializable]
    public class JsonWaves{
        public List<JsonEnemy> ennemies = new List<JsonEnemy>();
        public List<JsonWave> waves;

        [System.Serializable]
        public class JsonEnemy{
            public string name;
            public float hp;
            public float speed;
            public float value;
            public string texture;
            public string attribute;
        }
        [System.Serializable]
        public class JsonWave{
            public string waveName;
            public List<JsonUnitInfos> unitInfos = new List<JsonUnitInfos>();

            [System.Serializable]
            public class JsonUnitInfos {
                public string unitName;
                public int howMany;
                public float releaseSpeed;
            }
        }
    }

    [System.Serializable]
    public class JsonDefenses{
        
         public List<JsonTower> towers = new List<JsonTower>();

        [System.Serializable]
        public class JsonTower{
            public string name;
            public string description;
            public List<string> target = new List<string>();
            public List<JsonTowerLevel> level = new List<JsonTowerLevel>();

            [System.Serializable]
            public class JsonTowerLevel{
                public float cost;
                public string name;
                public float power;
                public float radius;
                public float attackSpeed;
                public string texture;
                public string projectile;
            }
        }
    }

}
