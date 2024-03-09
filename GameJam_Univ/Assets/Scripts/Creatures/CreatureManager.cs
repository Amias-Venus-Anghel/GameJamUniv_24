using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    // first string is merged between color codes, second string is resulting color code
    private Dictionary<string, string> combinationsDict;
    private Dictionary<string, Sprite> colorsDict;
    private Dictionary<string, float> powerDict;
    private Dictionary<string, float> lifeTimeDict;
    private Dictionary<string, float> fireRateDict;

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
        public float power;
        public float fireRate;
        public float lifeTime;
    }

    [SerializeField] private colorCombination[] combinations = null;
    [SerializeField] private colorSprites[] sprites = null;
    [SerializeField] private string[] baseCodes = null;

    void Start() {
        /* adding color combiantions to dictionary */
        combinationsDict = new Dictionary<string, string>();

        foreach (colorCombination c in combinations) {
            string code = c.colorCode1 + c.colorCode2;
            combinationsDict.Add(code, c.colorCodeResult);
        }

        /* adding color sprites and power to dictionary */
        colorsDict = new Dictionary<string, Sprite>();
        powerDict = new Dictionary<string, float>();
        fireRateDict = new Dictionary<string, float>();
        lifeTimeDict = new Dictionary<string, float>();

        foreach (colorSprites s in sprites) {
            colorsDict.Add(s.colorCode, s.sprite);
            powerDict.Add(s.colorCode, s.power);
            lifeTimeDict.Add(s.colorCode, s.lifeTime);
            fireRateDict.Add(s.colorCode, s.fireRate);
        }

        // assign random color codes
        for (int i = 0; i < transform.childCount; i++) {
            int index = UnityEngine.Random.Range(0, 3);
            string color = baseCodes[index];
            transform.GetChild(i).GetComponent<CreatureMerge>().SetColorCode(color, colorsDict[color]);
            SetStatsForCode(color, transform.GetChild(i).gameObject);
        }

        // random select number of creatures on hexagon
        int keep = UnityEngine.Random.Range(1, 4);

        HashSet<int> keep_index = new HashSet<int>();

        while (keep_index.Count < keep) {
            int index = UnityEngine.Random.Range(0, 6);
            keep_index.Add(index);
        }

        for (int i = 0; i < 6; i++) {
            if (keep_index.Contains(i)) {
                continue;
            }
            Destroy(transform.GetChild(i).gameObject);
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

    public void SetStatsForCode(string color_code, GameObject creature) {
        if (!colorsDict.ContainsKey(color_code)) {
            Debug.Log("Missing color " + color_code);
            return;
        }
            
        Attack attack = creature.GetComponent<Attack>();
        attack.SetPowers(powerDict[color_code], lifeTimeDict[color_code], fireRateDict[color_code]);
    }
}
