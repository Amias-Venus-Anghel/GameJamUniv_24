using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreatureManager;

public class DifficultySetting : MonoBehaviour
{
    [SerializeField] private creatureStats[] easy = null;
    [SerializeField] private creatureStats[] normal = null;
    [SerializeField] private creatureStats[] hard = null;

    private int difficulty;

    public static DifficultySetting instance;

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

    public void SetDifficulty(int diffCode) {
        difficulty = diffCode;
        FindObjectOfType<MainMenu>().PlayGame();
    }

    public creatureStats[] GetDifficultySettings() {
        switch (difficulty) {
            case 0:
                Debug.Log("difficulty: easy");
                return easy;
            case 2: 
                Debug.Log("difficulty: hard");
                return hard;
            default: 
                Debug.Log("difficulty: normal");
                return normal;
        }
    }

}
