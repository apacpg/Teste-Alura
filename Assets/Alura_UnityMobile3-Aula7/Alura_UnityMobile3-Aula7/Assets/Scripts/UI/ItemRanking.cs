using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRanking : MonoBehaviour
{
    [SerializeField]
    private Text textoDaColocacao;

    [SerializeField]
    private Text textoDoNome;

    [SerializeField]
    private Text textoDaPontuacao;

    public void Configurar(int colocacao, string nome, int pontos)
    {
        this.textoDaColocacao.text = colocacao.ToString();
        this.textoDoNome.text = nome;
        this.textoDaPontuacao.text = pontos.ToString();
    }

}
