using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class ControlaAudio : MonoBehaviour
{
    [SerializeField]
    private AtualizarSlider AtualizaSlider;

    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private string parametroDoVolume;

    private float volumePadrao = 80;

    private void Start()
    {
        if (PlayerPrefs.HasKey(this.parametroDoVolume))
        {
            float db = PlayerPrefs.GetFloat(this.parametroDoVolume) - 80f;
            this.mixer.SetFloat(this.parametroDoVolume, db);
            AtualizaSlider.Invoke(PlayerPrefs.GetFloat(this.parametroDoVolume));
        }
        
        else
        {
            float db = volumePadrao - 80f;
            this.mixer.SetFloat(this.parametroDoVolume, db);
            AtualizaSlider.Invoke(volumePadrao);
        }
    }

    public void MudarVolume(float volume)
    {
        float db = volume - 80f;
        this.mixer.SetFloat(this.parametroDoVolume, db);
        PlayerPrefs.SetFloat(this.parametroDoVolume, volume);
    }
    
}

[Serializable]
public class AtualizarSlider : UnityEvent<float> { }
