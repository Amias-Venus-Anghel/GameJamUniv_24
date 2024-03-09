using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Transform[] enemies = null;
    public Transform spawnPoint;
    public float spawnTime = 5f;
    public int totalWaves = 5;
    private int waveNo = 1;
    private float countdown = 0f;

    private int deadEnemies = 0;

    private bool timeToSpawn = false;

    private List<Transform> spawned_enemies = null;

    public void spawnEnemyWaves(int nrOfWaves, float spawnSpeed = 5) {
        waveNo = 1;
        deadEnemies = 0;
        totalWaves = nrOfWaves;
        spawnTime = spawnSpeed;
        timeToSpawn = true;
        spawned_enemies = new List<Transform>();
    }        
    
    // clean enemies when new round
    public void DestroyLeftovers() {
        if (spawned_enemies != null) {
            foreach(Transform enemy in spawned_enemies) {
                if (enemy != null) {
                    Destroy(enemy.gameObject);
                }
            }
        }
    }

    void Update() {
        if(timeToSpawn && waveNo <= totalWaves) {
            if(countdown <= 0f) {
                spawnWave();
                countdown = spawnTime;
            }
            countdown -= Time.deltaTime;
        }

       if (deadEnemies == totalWaves) {
            GetComponent<GameMaster>().AllEnemiesKilled();
       }
    }

    // called when enemy is destroyed by end point or bullets
    public void EnemyDied() {
        deadEnemies++;
    }

    void spawnWave() {
        //for(int i = 0; i < waveNo; i++) {
            spawnEnemy(); // nr de enemies intr-un wave: 1
        //}
        waveNo++;
    }

    void spawnEnemy(){
        int index = Random.Range(0, enemies.Length);
        Transform enemy = Instantiate(enemies[index], spawnPoint.position, spawnPoint.rotation);
        spawned_enemies.Add(enemy);
    }
}
