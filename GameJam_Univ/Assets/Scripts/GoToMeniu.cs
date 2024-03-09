using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMeniu : MonoBehaviour
{
    public void MeniuGame()
   {
    SceneManager.LoadSceneAsync(0);
   }
}
