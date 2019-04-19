using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnEnemy : MonoBehaviour
{
    //public Transform enemy;

    //public Transform spawnPoint;
    //
    private WaveFactory factory;

    public int timeBetweenWaves = 10;

    private bool countDownStarted = false;
    private bool stopSignal = false;

    void Start(){
        factory = gameObject.GetComponent<WaveFactory>();
    }

    void FixedUpdate(){
        if(stopSignal) return;
        if(factory == null) {
            factory = gameObject.GetComponent<WaveFactory>();
            return;
        }
        if(!factory.hasInitialized) return;
        if(factory.isSpawningWave) return;
        if(!factory.hasMoreWaves()){
            CheckWin();
            return;
        }
        if(!countDownStarted){
            Debug.Log("Spawning Wave in " + timeBetweenWaves);
            countDownStarted = true;
            StartCoroutine(Countdown(timeBetweenWaves));
        }
    }

    private IEnumerator Countdown(int nbSeconds){
        yield return new WaitForSecondsRealtime(nbSeconds);
        countDownStarted = false;
        factory.BuildNextWave();
        yield return new WaitForFixedUpdate();
    }

    private void CheckWin(){
        EnemyHealth[] enemies = GameObject.FindObjectsOfType<EnemyHealth>();
        if(enemies.Length == 0) GameObject.FindObjectOfType<EndGame>().Win();
    }

    public void StopSpawning(){
        stopSignal = true;
        StopAllCoroutines();
        factory.StopSpawning();
    }
}
