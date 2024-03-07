using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    // first string is merged between color codes, second string is resulting color code
    private Dictionary<string, string> combinationsDict;
    private Dictionary<string, Sprite> colorsDict;

    [Serializable]
    public struct colorCombination {
        public string colorCode1;
        public string colorCode2;
        public string colorCodeResult;
    }

    [Serializable]
    public struct colorSprites {
        public string colorCode;
        public Sprite sprite;
    }

    [SerializeField] private colorCombination[] combinations;
    [SerializeField] private colorSprites[] sprites;
    [SerializeField] private string[] baseCodes;

    void Start() {
        /* adding color combiantions to dictionary */
        combinationsDict = new Dictionary<string, string>();

        foreach (colorCombination c in combinations) {
            string code = c.colorCode1 + c.colorCode2;
            combinationsDict.Add(code, c.colorCodeResult);
        }

        /* adding color sprites to dictionary */
        colorsDict = new Dictionary<string, Sprite>();

        foreach (colorSprites s in sprites) {
            colorsDict.Add(s.colorCode, s.sprite);
        }

        // random select where creatures are on hexagon

        // assign random color codes
        for (int i = 0; i < transform.childCount; i++) {
            int index = UnityEngine.Random.RandomRange(0, 3);
            string color = baseCodes[index];
            transform.GetChild(i).GetComponent<CreatureMerge>().SetColorCode(color, colorsDict[color]);
        }
    }

    public string GetCombinationCode(string code1, string code2) {
        if (combinationsDict.ContainsKey(code1 + code2)) {
            return combinationsDict[code1 + code2];
        }
        else if (combinationsDict.ContainsKey(code2 + code1)) {
            return combinationsDict[code2 + code1];
        }
        else {
            return null;
        }
    }

    public Sprite GetSpriteOfCode(string color_code) {
        if (colorsDict.ContainsKey(color_code))
            return colorsDict[color_code];
        else {
            Debug.Log("missing sprite " + color_code);
            return null;
        }
    }
}
