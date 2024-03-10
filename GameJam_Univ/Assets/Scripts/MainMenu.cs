using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   public Slider backgroundSlider, soundEffectsSlider;
   private AudioManager audioManager = null;

   bool audioSet = false;
   private void Awake()
   {
      audioSet = false;
      audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
      audioSet = audioManager.SetSoundsValues( backgroundSlider, soundEffectsSlider );
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
      if (audioSet) {
         audioManager.UpdateSound();
      }
   }
}
