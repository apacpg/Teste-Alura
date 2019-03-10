using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDoJogador : Status
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("VidaDoJogador"))
        {
            this.Vida = PlayerPrefs.GetInt("VidaDoJogador"); // Acessa a vida o PlayerPref que contem a vida do jogador durante o jogo
        }

        else
        {
            this.Vida = this.VidaInicial;
        }
    }
}
