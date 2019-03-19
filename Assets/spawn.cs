using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spawn : MonoBehaviour
{
    public Transform ennemy;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
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
            SpawnEnnemy();
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    void SpawnEnnemy()
    {
        Instantiate(ennemy, spawnPoint.position, spawnPoint.rotation);
    }
}
