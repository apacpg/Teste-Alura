using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizadorDeInterface : MonoBehaviour
{
    private ControlaInterface Interface;

    private void Start()
    {
        Interface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    public void AtualizaQuantidadeDeZumbiesMortos(int zumbies)
    {
        Interface.AtualizarQuantidadeDeZumbisMortos(zumbies);
    }

    public void AtualizaBarraDeVidaDoJogador()
    {
        Interface.AtualizarSliderVidaJogador();
    }

    public void GameOver()
    {
        Interface.GameOver();
    }
}
