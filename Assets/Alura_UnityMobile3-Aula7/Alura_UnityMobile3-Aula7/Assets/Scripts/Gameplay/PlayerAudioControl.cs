using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioControl : MonoBehaviour
{
    private AudioSource playerAudio;

    private void Start()
    {
        playerAudio = GameObject.FindGameObjectWithTag("SFXAudio").GetComponent<AudioSource>();
    }

    public void TocarUmaVez(AudioClip clipe)
    {
        playerAudio.PlayOneShot(clipe);
    }
}
