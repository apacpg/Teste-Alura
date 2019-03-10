using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class ControleDoRanking : MonoBehaviour
{
    private StatusDoJogador statusDoJogador;

    void Start()
    {
        statusDoJogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<StatusDoJogador>();
    }

    public void Reiniciar()
    {
        PlayerPrefs.SetInt("PontuacaoDoJogador", 0);
        PlayerPrefs.SetInt("VidaDoJogador", statusDoJogador.VidaInicial);
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    
}
