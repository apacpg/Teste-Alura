using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private int quantidadeDeCura = 15;
    private int tempoDeDestruicao = 5;
    private AudioSource audio; // Encontra o AudioSource que ira reproduzir o som de ser coletado

    public AudioClip SomDePickUp;

    private void Start()
    {
        audio = GameObject.FindGameObjectWithTag("SFXAudio").GetComponent<AudioSource>();
        Destroy(gameObject, tempoDeDestruicao);
    }

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        if(objetoDeColisao.tag == "Jogador")
        {
            objetoDeColisao.GetComponent<ControlaJogador>().CurarVida(quantidadeDeCura);
            audio.PlayOneShot(SomDePickUp);
            Destroy(gameObject);
        }
    }
}
