using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FimdaFase : MonoBehaviour
{
    [SerializeField]
    private DarPontosBonus DarPontosBon; // Pontuuacao bonus quando o jogador passa de fase

    [SerializeField]
    private UnityEvent SalvarPontuacao; // Salva pontuacao

    private Status statusDoJogador; // Variavel que contem o StatusDoJogador

    public int pontosBonusPorAcabarAFase = 15; // Pontos Bonus que o jogador ganha ao passar de fase

    private void Start()
    {
        statusDoJogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Status>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Jogador")
        {
            PassarDeFase(); // Quando o jogador entra em contato com o fim da fase chama o metodo que leva para proxima fase
        }
    }

    private void PassarDeFase()
    {
        GerenteDePontuacao gerente = GameObject.FindObjectOfType(typeof(GerenteDePontuacao)) as GerenteDePontuacao; // Encontra o gerente de pontuacao
        DarPontosBon.Invoke(pontosBonusPorAcabarAFase); // Adiciona os pontos bonus para o jogador
        PlayerPrefs.SetInt("VidaDoJogador", statusDoJogador.Vida); // Salva a vida atual do jogador
        SalvarPontuacao.Invoke(); // Salva a pontuacao atual do jogador
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Carrega a proxima fase
    }

    [Serializable]
    public class DarPontosBonus : UnityEvent<int> { }
}
