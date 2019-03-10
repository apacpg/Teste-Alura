using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaPontuacao : MonoBehaviour
{
    [SerializeField]
    private TextoDinamico textoDePontuacao;

    [SerializeField]
    private Ranking ranking;

    private int id;

    private void Start()
    {
        int totalDePontos = PlayerPrefs.GetInt("PontuacaoDoJogador");

        this.textoDePontuacao.AtualizarTexto(totalDePontos);

        this.id = ranking.AdicionaPontuacao("Nome", totalDePontos);
    }

    public void AlterarNome(string nome)
    {
        this.ranking.AlterarNome(nome, id);
    }
}
