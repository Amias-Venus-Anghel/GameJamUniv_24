﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoseSound : MonoBehaviour
{
    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.lose);
    }
}
