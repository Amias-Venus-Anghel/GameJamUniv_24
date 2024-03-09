using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float life = 15f;
    [SerializeField] private int score = 20;

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            GameObject.Find("GameMaster").GetComponent<GameMaster>().AddScore(score);
            GameObject.Find("GameMaster").GetComponent<WaveSpawner>().EnemyDied();
            Destroy(this.gameObject);
        }
    }

    public void GetDamage(float power)
    {
        life -= power;
    }
}
