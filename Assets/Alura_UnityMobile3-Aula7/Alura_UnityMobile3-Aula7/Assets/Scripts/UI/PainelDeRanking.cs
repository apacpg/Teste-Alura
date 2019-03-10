using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelDeRanking : MonoBehaviour
{
    [Range(3, 10)]
    public int numeroMaximoDeInstanciasDoRanking = 5;

    [SerializeField]
    private GameObject prefabDoRanking;

    [SerializeField]
    private Transform painelDeColocacoes;

    [SerializeField]
    private Ranking ranking;

    private void Start()
    {
        var listaDeColocados = ranking.GetColocados();
        for (int i = 0; i < listaDeColocados.Count; i++)
        {
            if (i >= numeroMaximoDeInstanciasDoRanking)
                break;

            GameObject colocado = GameObject.Instantiate(prefabDoRanking, painelDeColocacoes);
            int posicao = i + 1;
            colocado.GetComponent<ItemRanking>().Configurar((posicao), listaDeColocados[i].nome, listaDeColocados[i].pontuacao);
        }
    }
}
