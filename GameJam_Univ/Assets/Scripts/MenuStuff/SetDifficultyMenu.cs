using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDifficultyMenu : MonoBehaviour
{
    DifficultySetting setting;

    void Start() {
        setting = FindObjectOfType<DifficultySetting>();
    }

    public void SetDifficulty(int dif){
        setting.SetDifficulty(dif);
    }
}
