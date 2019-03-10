using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces : MonoBehaviour
{
    private GerenteDePontuacao gerenteDePontuacao;

    // Start is called before the first frame update
    void Start()
    {
        gerenteDePontuacao = GameObject.FindObjectOfType(typeof(GerenteDePontuacao)) as GerenteDePontuacao; 
        // Encontra a interface grafica do jogo
    }

    public void AtualizaPontuacao() // Atualiza a pontuacao
    {
        gerenteDePontuacao.AtualizaPontuacao();
    }
}
