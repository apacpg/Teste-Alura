using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections.ObjectModel;
using System;

public class Ranking : MonoBehaviour
{
    private static string NOME_DO_ARQUIVO = "Ranking.json";

    [SerializeField]
    private List<Colocado> listaDeColocados;

    private string pathParaOArquivo;

    private void Awake()
    {
        pathParaOArquivo = Path.Combine(Application.persistentDataPath, NOME_DO_ARQUIVO);
        if (File.Exists(pathParaOArquivo))
        {
            var textoJson = File.ReadAllText(pathParaOArquivo);
            JsonUtility.FromJsonOverwrite(textoJson, this);
        }
        else
        {
            this.listaDeColocados = new List<Colocado>();
        }
    }

    private void SalvaRankingDePontuacao()
    {
        var textoJson = JsonUtility.ToJson(this);
        
        File.WriteAllText(pathParaOArquivo, textoJson);
        Debug.Log(pathParaOArquivo);
    }

    public int AdicionaPontuacao(string nome, int pontos)
    {
        var id = listaDeColocados.Count * UnityEngine.Random.Range(1, 100000000);
        Colocado novoColocado = new Colocado(nome, pontos, id);
        this.listaDeColocados.Add(novoColocado);
        this.listaDeColocados.Sort();
        SalvaRankingDePontuacao();
        return id;
    }

    public int Quantidade()
    {
        return this.listaDeColocados.Count;
    }

    public ReadOnlyCollection<Colocado> GetColocados()
    {
        return this.listaDeColocados.AsReadOnly();
    }

    public void AlterarNome(string novoNome, int id)
    {
        foreach(var item in this.listaDeColocados)
        {
            if(item.id == id)
            {
                item.nome = novoNome;
                break;
            }
        }
        SalvaRankingDePontuacao();
    }
}

[System.Serializable]
public class Colocado : IComparable
{
    public string nome;
    public int pontuacao;
    public int id;

    public Colocado(string nome, int pontuacao, int id)
    {
        this.nome = nome;
        this.pontuacao = pontuacao;
        this.id = id;
    }

    public int CompareTo(object obj)
    {
        var outroObjeto = obj as Colocado;
        return outroObjeto.pontuacao.CompareTo(this.pontuacao);
    }
}
