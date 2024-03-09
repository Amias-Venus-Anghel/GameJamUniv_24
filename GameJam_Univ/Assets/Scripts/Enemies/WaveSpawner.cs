using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Transform[] enemies = null;
    public Transform spawnPoint;
    public float spawnTime = 5f;
    public float totalWaves = 5;
    private int waveNo = 1;
    private float countdown = 2f;

    private GameObject hexagonEnd = null;
    private HexagonDragDrop hexagonEndScript;
    private bool timeToSpawn = false;

    public void spawnEnemyWaves() {
        timeToSpawn = true;
    }        
    
    void Update() {
        if(timeToSpawn && waveNo <= totalWaves) {
            if(countdown <= 0f) {
                Debug.Log("yup45");
                spawnWave();
                countdown = spawnTime;
            }
            countdown -= Time.deltaTime;
        }
    }

    void spawnWave() {
        Debug.Log("enemy wave: " + waveNo);
        //for(int i = 0; i < waveNo; i++) {
            spawnEnemy(); // nr de enemies intr-un wave: 1
        //}
        waveNo++;
    }

    void spawnEnemy(){
        int index = Random.RandomRange(0, enemies.Length);
        Transform enemy = Instantiate(enemies[index], spawnPoint.position, spawnPoint.rotation);

    }
}
