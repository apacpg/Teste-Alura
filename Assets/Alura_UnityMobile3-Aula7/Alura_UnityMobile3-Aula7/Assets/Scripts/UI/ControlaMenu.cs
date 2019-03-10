using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour
{
    public GameObject BotaoSair;
    public GameObject menuInicial;
    public GameObject menuDeOpcoes;

    private StatusDoJogador statusDoJogador;

    private void Start()
    {
#if UNITY_STANDALONE || UNITY_EDITOR // Atica o botao de sair do jogo
        BotaoSair.SetActive(true);
#endif

        statusDoJogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<StatusDoJogador>(); 
        // Armazena o scrip de StatusDoJogador 
    }

    public void JogarJogo() // Inicia o jogo na primeira fase quando aperta o botao Jogar
    {
        ReiniciarVidaEPontuacao();
        StartCoroutine(MudarCena(1));
    }

    IEnumerator MudarCena(int index)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadScene(index);
    }

    public void SairDoJogo()
    {
        ReiniciarVidaEPontuacao();
        StartCoroutine(Sair());
    }

    public void IrParaMenuDeOpcoes() // Abre o menu de opcoes que controla o volume dos audios
    {
        this.menuInicial.SetActive(false);
        this.menuDeOpcoes.SetActive(true);
    }

    public void SairDoMenuDeOpcoes() // Fecha o menu de opcoes e abre o menu principal
    {
        this.menuDeOpcoes.SetActive(false);
        this.menuInicial.SetActive(true);
    }

    IEnumerator Sair ()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void ReiniciarVidaEPontuacao() // Zera a pontuacao e a vida do jogador
    {
        PlayerPrefs.SetInt("VidaDoJogador", statusDoJogador.VidaInicial);
        PlayerPrefs.SetInt("PontuacaoDoJogador", 0);
    }
}
