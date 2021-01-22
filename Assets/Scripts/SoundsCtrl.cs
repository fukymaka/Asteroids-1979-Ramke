using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundsCtrl : MonoBehaviour
{
    static public SoundsCtrl S;

    [Header("Set in Inspector")]
    public AudioSource MainThemeMusic;
    public AudioSource heroShotSound;
    public AudioSource heroDeathSound;
    public AudioSource thrustSound;
    public AudioSource asteroidExplosionSound;
    public AudioSource ufoDeathSound;
    public AudioSource ufoShotSound;
    public AudioSource ufoComingSound;
    public AudioSource clickSound;
    public AudioMixerGroup mixer;


    private void Awake()
    {
        S = this;
    }

    public void PlayMainThemeMusic(bool enabled)
    {
        if (enabled)
        {
            MainThemeMusic.Stop();
        }
        else
        {
            MainThemeMusic.Play();
        }
    }

    public void PlayEffectsSound(bool enabled)
    {
        if (enabled)
        {
            mixer.audioMixer.SetFloat("EffectsVolume", -80);
        }
        else
        {
            mixer.audioMixer.SetFloat("EffectsVolume", 0);
        }
    }

    public void PlayHeroShotSound()
    {
        heroShotSound.pitch = Random.Range(0.8f, 1f);
        heroShotSound.Play();
    }


    public void PlayDeathHeroSound()
    {
        heroDeathSound.Play();
    }


    public void PlayThrustSound(bool enabled)
    {
        if (enabled)
        {
            thrustSound.gameObject.SetActive(true);
        }
        else
        {
            thrustSound.gameObject.SetActive(false);
        }
    }


    public void PlayAsteroidExplosionSound()
    {
        asteroidExplosionSound.volume = Random.Range(0.2f, 0.6f);
        asteroidExplosionSound.pitch = Random.Range(0.7f, 0.8f);
        asteroidExplosionSound.Play();
    }

    
    public void PlayUfoDeathSound()
    {
        ufoDeathSound.Play();
        ufoComingSound.Stop();
    }


    public void PlayUfoShotSound()
    {
        ufoShotSound.Play();
    }


    public void PlayUfoComingSound()
    {
        ufoComingSound.Play();
    }


    public void PlayClickSound()
    {
        clickSound.Play();
    }
}
