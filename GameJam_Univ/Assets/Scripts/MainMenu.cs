using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   public Slider backgroundSlider, soundEffectsSlider;
   private AudioManager audioManager = null;
   private void Awake()
   {
      audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
      audioManager.SetSoundsValues( backgroundSlider, soundEffectsSlider );
   }
   public void PlayGame()
   {
    SceneManager.LoadSceneAsync(1);
   }

   public void QuitGame()
   {
      Application.Quit();
   }

   public void UpdateSound()
   {
           audioManager.UpdateSound();
   }
}
