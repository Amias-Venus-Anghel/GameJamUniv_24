using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataBringer : MonoBehaviour
{
    public static DataBringer instance;
    public float finalScore = 0;

    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EndGame(float score) {
        finalScore = score;

        if (finalScore > 0) {
            // load good ending
            SceneManager.LoadSceneAsync(4);
        }
        else {
            // load bad ending
            SceneManager.LoadSceneAsync(3);
        }
    }
}
