using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoDinamico : MonoBehaviour
{
    private Text texto;

    private void Awake()
    {
        texto = GetComponent<Text>();
    }

    public void AtualizarTexto(int novoTexto)
    {
        this.texto.text = novoTexto.ToString();
    }

    public void AtualizarTexto(string novoTexto)
    {
        this.texto.text = novoTexto;
    }
}
