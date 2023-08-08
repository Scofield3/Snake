using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip AppleClip;

    public AudioClip GameOverClip;

    private AudioSource appleAudioSource;

    private AudioSource gameOverAudioSource;

    void Awake()
    {
        if (this.AppleClip != null)
        {
            this.appleAudioSource = this.gameObject.AddComponent<AudioSource>();
            this.appleAudioSource.clip = this.AppleClip;

        }
        if (this.GameOverClip != null)
        {
            this.gameOverAudioSource = this.gameObject.AddComponent<AudioSource>();
            this.gameOverAudioSource.clip = this.GameOverClip;
        }
    }

    public void PlayAppleSoundEffect()
    {
        if (this.appleAudioSource != null)
        {
            this.appleAudioSource.Play();
        }
    }

    public void PlayGameOverSoundEffect()
    {
        if (this.gameOverAudioSource != null)
        {
            this.gameOverAudioSource.Play();
        }
    }

    void Update()
    {

    }
}
