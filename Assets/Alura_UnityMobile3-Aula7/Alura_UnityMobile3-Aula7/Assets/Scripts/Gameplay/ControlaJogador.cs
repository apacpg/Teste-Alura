using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// FORCA A IMPLEMENTACAO DOS SCRIPTS AO JOGADOR
[RequireComponent(typeof(MovimentoJogador))] //Script responsavel por mover o jogador
[RequireComponent(typeof(AnimacaoPersonagem))] //Script responsavel por animar o personagem
[RequireComponent(typeof(StatusDoJogador))] //Script responsavel por conter e alterar os status do personagem
[RequireComponent(typeof(ControlaArma))] //Script responsavel por receber os inputs de utilização da arma 
public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{
    private StatusDoJogador statusJogador;

    private Vector3 direcao;
    private MovimentoJogador meuMovimentoJogador;

    [SerializeField]
    private Movimento MovimentoDoJogador; // Evento que chama os metodos de MovimentoJogador para mover o personagem

    [SerializeField]
    private AnimacaoDeMovimentoDoJogador AnimacaoDeMovimento; // Evento que chama os metodos de Animacao de movimento do personagem

    [SerializeField]
    private UnityEvent Morte; // Evento que chama os metodos do StatusDoJogador quando ele morre

    [SerializeField]
    private PlayOneShot PlayAudio; // Evento que chama os metodo OneShot para reproduzir o audio do objeto GerenteDeAudio

    [SerializeField]
    private PerdeVida LevaDano; // Evento que chama os metodos de levar dano do script StatusDoJogador

    [SerializeField]
    private RecuperaVida RecuperaVida; // Evento que chama os metodos de recuperar vida do script StatusDoJogador

    public LayerMask MascaraChao;
    public AudioClip SomDeDano;
    

    private void Start()
    {
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        statusJogador = GetComponent<StatusDoJogador>();
    }

    void Update()
    {
        AnimacaoDeMovimento.Invoke(meuMovimentoJogador.GetMagnitudeMovimento());
        //animacaoJogador.Movimentar(this.meuMovimentoJogador.Direcao.magnitude);
    }

    void FixedUpdate()
    {
        MovimentoDoJogador.Invoke(statusJogador.Velocidade);
        //meuMovimentoJogador.Movimentar(statusJogador.Velocidade);

        //meuMovimentoJogador.RotacaoJogador();
    }

    public void TomarDano (int dano)
    {
        LevaDano.Invoke(dano); // Repassar as funcoes de levar dano para o script responssavel pelos status do personagem
        PlayAudio.Invoke(SomDeDano);
        //audio.PlayOneShot(SomDeDano);
    }

    public void Morrer ()
    {
        Morte.Invoke();
        //scriptControlaInterface.GameOver();
    }

    public void CurarVida (int quantidadeDeCura)
    {
        RecuperaVida.Invoke(quantidadeDeCura);
    }
}

#region Eventos

[Serializable]
 public class Movimento : UnityEvent<float> { }

[Serializable]
public class AnimacaoDeMovimentoDoJogador : UnityEvent<float> { }

[Serializable]
public class RecuperaVida : UnityEvent<int> { }

[Serializable]
public class PerdeVida : UnityEvent<int> { }

[Serializable]
public class PlayOneShot : UnityEvent<AudioClip> { }

#endregion