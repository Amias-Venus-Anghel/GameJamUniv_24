using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Transform[] enemies = null;
    public Transform spawnPoint;
    public float spawnTime = 5f;
    private int waveNo = 1;
    private float countdown = 2f;

    private GameObject hexagonEnd = null;
    private HexagonDragDrop hexagonEndScript;

    void Update() {
        hexagonEnd = GameObject.FindGameObjectWithTag("Endpoint");
        Debug.Log(hexagonEnd.gameObject.name);
        hexagonEndScript = hexagonEnd.GetComponent<HexagonDragDrop>();
        if(hexagonEndScript.placed) {
             if(countdown <= 0f) {
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
