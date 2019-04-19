using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFactory : MonoBehaviour {

    public int waveIndex = 0;
    private List<LoadData.JsonWaves.JsonWave> waves;
    private EnemyFactory factory;
    public bool hasInitialized {get; private set;} = false;
    public bool isSpawningWave {get; private set;} = false;

    void Start(){
        LoadData.JsonWaves jw = LoadData.LoadWaves("Json/ennemyWaves");
        factory = new EnemyFactory(jw.ennemies);
        waves = jw.waves;
        hasInitialized = true;
    }

    public bool hasMoreWaves(){
        return waveIndex < waves.ToArray().Length;
    }

    public void BuildNextWave(){
        if(!hasMoreWaves() || isSpawningWave) return;
        Build(waves[waveIndex]);
        Debug.Log("Building Wave " + waves[waveIndex].waveName);
        ++waveIndex;
    }

    public void StopSpawning(){
        isSpawningWave = false;
    }

    private void Build(LoadData.JsonWaves.JsonWave waveJson){
        if(waveJson == null) return;
        isSpawningWave = true;
        StartCoroutine(SpawnWave(waveJson));
    }

    private IEnumerator SpawnWave(LoadData.JsonWaves.JsonWave waveJson){
        foreach(var infos in waveJson.unitInfos){
            GameObject unit = factory.Build(infos.unitName);
            if(unit == null) continue;
            float releaseSpeed = infos.releaseSpeed/1000;
            for(int i = 0; i < infos.howMany; ++i){
                if(!isSpawningWave) break;
                Instantiate(unit, transform.position, transform.rotation);
                yield return new WaitForSeconds(releaseSpeed);
            }
        }
        StopSpawning();
    }

}