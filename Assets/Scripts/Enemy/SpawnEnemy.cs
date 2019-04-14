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
        if(!factory.hasMoreWaves()) return; // partie gagnée à la mort du derneir ennemi
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

    public void StopSpawning(){
        stopSignal = true;
        StopAllCoroutines();
        factory.StopSpawning();
    }

    /*public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private float time = 30f; 


    private int waveIndex = 1;

    void Update()
    {
        if(countdown <= 0f && time >= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        time -= Time.deltaTime;
        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnAnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        ++waveIndex;
    }

    void SpawnAnEnemy()
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }*/
}
