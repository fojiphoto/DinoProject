using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSoundsHandler : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip idleClip, attackClick, deathClip;
    public AudioClip[] hitClips;

    private void Start()
    {
        audioSource.mute = !PreferenceManager.isSoundOn;
    }

    public void PlayDinoSound(DinoSound sound)
    {
        if (PreferenceManager.isSoundOn)
        {
            AudioClip clip = null;
            if (sound == DinoSound.HURT)
            {
                var rand = Random.Range(0, hitClips.Length);
                clip = hitClips[rand];
            }
            else if (sound == DinoSound.IDLE)
            {
                clip = idleClip;
            }
            else if (sound == DinoSound.ATTACK)
            {
                clip = attackClick;
            }
            else if (sound == DinoSound.DEATH)
            {
                clip = deathClip;
            }
            audioSource.PlayOneShot(clip);
        }
    }
}