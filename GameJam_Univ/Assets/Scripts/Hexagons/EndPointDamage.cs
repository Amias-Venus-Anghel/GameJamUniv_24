using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPointDamage : MonoBehaviour
{
    [SerializeField] private Sprite happyEnd;
    [SerializeField] private Sprite hurtEnd;

    private Image image;

    void Start() {
        image = GetComponent<Image>();
    }

    public void HurtHeartAnimate() {
        StartCoroutine(ChangeSprite());
    }

    private IEnumerator ChangeSprite() {
        image.sprite = hurtEnd;
        // play hurt sound here
        yield return new WaitForSeconds(0.5f);
        image.sprite = happyEnd;
    }
}
