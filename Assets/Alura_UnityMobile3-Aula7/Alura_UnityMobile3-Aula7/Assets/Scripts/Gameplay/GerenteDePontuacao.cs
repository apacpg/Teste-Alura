using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GerenteDePontuacao : MonoBehaviour
{
    [SerializeField]
    private AtualizaInterface AtualizaPontuacaoNaInterface; // Evento que atualiza a pontuacao na interface do jogo

    public int pontuacao; // Pontuacao do jogador

    private void Start()
    {
        this.pontuacao = PlayerPrefs.GetInt("PontuacaoDoJogador"); // Adiquire a pontuacao da partida do jogador
        AtualizaPontuacaoNaInterface.Invoke(this.pontuacao);
    }

    public void AtualizaPontuacao()
    {
        this.pontuacao++;
        AtualizaPontuacaoNaInterface.Invoke(this.pontuacao);
    }

    public void AtualizaPontuacao(int novosPontos) // Metodo que adiciona diversos pontos de uma so vez a pontuacao
    {
        this.pontuacao += novosPontos;
        AtualizaPontuacaoNaInterface.Invoke(this.pontuacao);
    }

    public void SalvarPontuacao() // Salva a pontuacao da partida no PlayerPrefs
    {
        PlayerPrefs.SetInt("PontuacaoDoJogador", this.pontuacao);
    }
}

[Serializable]
public class AtualizaInterface : UnityEvent<int> { }
