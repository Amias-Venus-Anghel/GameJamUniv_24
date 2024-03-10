﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButtonSound : MonoBehaviour
{
    AudioManager audioManager;

    void Start() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PlayButtonPressSound(){
        audioManager.PlaySFX(audioManager.button_press);
    }
}
