using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    // first string is merged between color codes, second string is resulting color code
    private Dictionary<string, string> colorCombinations;

    [Serializable]
    public struct colorCombination {
        public string colorCode1;
        public string colorCode2;
        public string colorCodeResult;
    }

    public colorCombination[] combinations;

    void Start() {
        /* adding color combiantions to dictionary */
    }
}
