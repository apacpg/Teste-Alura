using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour{

    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoPontuacaoMaxima;
    public Text TextoQuantidadeDeZumbisMortos;
    public Text TextoChefeAparece;
    public Button BotaoDeTrocarArmas;
    public Text TextoDeObjetivo;

    private float tempoPontuacaoSalvo;
    private StatusDoJogador scriptStatus;


    // Use this for initialization
    void Start ()
    {
        #if UNITY_ANDROID || UNITY_IOS
                    BotaoDeTrocarArmas.gameObject.SetActive(true);
        #endif

        scriptStatus = GameObject.FindWithTag("Jogador").GetComponent<StatusDoJogador>();

        SliderVidaJogador.maxValue = scriptStatus.VidaInicial;
        AtualizarSliderVidaJogador();

        Time.timeScale = 1;

        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");

        DeterminarOObjetivo(); // Determina qual o objetivo de acordo com a fase
        AparecerTextoDeObjetivo(); // Mostra o texto de objetivo na tela no inicio da fase
    }

    public void AtualizarSliderVidaJogador ()
    {
        SliderVidaJogador.value = scriptStatus.Vida;
    }

    public void AtualizarQuantidadeDeZumbisMortos (int zumbisMortos) // Recebe o numero de zumbis mortos do GerenteDePontuacao
    {
        TextoQuantidadeDeZumbisMortos.text = string.Format("x {0}", zumbisMortos);
    }

    public void GameOver ()
    {
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        TextoTempoDeSobrevivencia.text = 
            "Você sobreviveu por " + minutos + "min e " + segundos + "s";

        AjustarPontuacaoMaxima(minutos, segundos);
    }

    void AjustarPontuacaoMaxima (int min, int seg)
    {
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = 
                string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo);
        }
        if(TextoPontuacaoMaxima.text == "")
        {
            min = (int)tempoPontuacaoSalvo / 60;
            seg = (int)tempoPontuacaoSalvo % 60;
            TextoPontuacaoMaxima.text =
                string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);
        }
    }

    public void Reiniciar () // Botao que reinicia o jogo
    {
        ReiniciarVidaEPontuacao(); // Reseta a pontuacao e a vida quando comeca um jogo novo
        SceneManager.LoadScene(1);
    }

    public void MenuDeRanking() // Botao que leva para o menu de ranking
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
    }

    public void AparecerTextoChefeCriado ()
    {
        StartCoroutine(DesaparecerTexto(2, TextoChefeAparece));
    }

    public void AparecerTextoDeObjetivo() // Mostra o texto de objetivo
    {
        StartCoroutine(DesaparecerTexto(3, TextoDeObjetivo));
    }

    IEnumerator DesaparecerTexto (float tempoDeSumico, Text textoParaSumir)
    {
        textoParaSumir.gameObject.SetActive(true);
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;
        while (textoParaSumir.color.a > 0)
        {
            contador += Time.deltaTime / tempoDeSumico;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corTexto;
            if(textoParaSumir.color.a <= 0)
            {
                textoParaSumir.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    private void DeterminarOObjetivo() //Determina o objetivo dependendo da fase
    {
        int faseAtual = SceneManager.GetActiveScene().buildIndex;
        if (faseAtual == 1)
        {
            this.TextoDeObjetivo.text = "Um portal para a proxima fase apareceu!";
        }
        else if(faseAtual == 2)
        {
            this.TextoDeObjetivo.text = "Sobreviva!";
        }
    }

    private void ReiniciarVidaEPontuacao() // Reseta a vida e a pontuacao que sera linkada ao jogador no inicio da fase
    {
        PlayerPrefs.SetInt("VidaDoJogador", scriptStatus.VidaInicial);
        PlayerPrefs.SetInt("PontuacaoDoJogador", 0);
    }
}
