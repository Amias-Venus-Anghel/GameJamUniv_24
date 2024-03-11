using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("----------- Audio Source ------------")]
   [SerializeField] AudioSource musicSource;
   [SerializeField] AudioSource SFXSource;

    [Header("----------- Audio Clip ------------")]
   public AudioClip background;
   public AudioClip attack;
   public AudioClip combine;
   public AudioClip placed;
   public AudioClip rotated;
   public AudioClip startingRound;
   public AudioClip win;
   public AudioClip lose;
   public AudioClip wrong_place;
   public AudioClip button_press;
   public static AudioManager instance;
   private static readonly string FirstPlay = "FirstPlay";
   private static readonly string BackgroundPref = "BackgroundPref";
   private static readonly string SoundEffectsPref = "SoundEffectsPref";
   private int firstPlayInt;
   public Slider backgroundSlider, soundEffectsSlider;
   private float backgroundFloat, soundEffectsFloat;
   private float backgroundValue, soundEffectsValue;
   private void Awake()
   {
     if(instance == null)
     {
          instance=this;
          DontDestroyOnLoad(gameObject);
     }
     else
     {
          Destroy(gameObject);
     }
     
   }

   private void Start()
   {
        musicSource.clip=background;
        musicSource.Play();

        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

          backgroundFloat = 1f;
          soundEffectsFloat = 1f;

          backgroundValue = backgroundFloat;
          soundEffectsValue = soundEffectsFloat;
          
          if (backgroundSlider != null) {
               backgroundSlider.value = backgroundFloat;
               soundEffectsSlider.value = soundEffectsFloat;
          }
   }

   public void PlaySFX(AudioClip clip)
   {
          SFXSource.PlayOneShot(clip);
   }

   public void UpdateSound()
     {
          musicSource.volume = backgroundSlider.value;
          SFXSource.volume = soundEffectsSlider.value;

          backgroundValue = backgroundSlider.value;
          soundEffectsValue = soundEffectsSlider.value;
     }

     public bool SetSoundsValues( Slider backgroundSlider, Slider soundEffectsSlider )
     {
          this.backgroundSlider = backgroundSlider;
          this.soundEffectsSlider = soundEffectsSlider;

          backgroundSlider.value = backgroundValue;
          soundEffectsSlider.value = soundEffectsValue;

          return true;
     }

     
   
}
