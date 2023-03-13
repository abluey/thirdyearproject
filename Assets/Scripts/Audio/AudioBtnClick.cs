using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBtnClick : MonoBehaviour
{
    public AudioSource clickNoise;

    public void PlaySound() {
        StartCoroutine(EnsurePlayed());
    }

    private IEnumerator EnsurePlayed() {
        clickNoise.Play();
        yield return new WaitForSeconds(0.5f);
    }
}
