using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoingToMenu : MonoBehaviour
{
     public void MenuGame()
   {
      SceneManager.LoadSceneAsync(0);
   }
}
