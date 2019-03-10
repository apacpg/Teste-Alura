using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtualizaNome : MonoBehaviour
{
    private Text textoDoNome;

    // Start is called before the first frame update
    void Awake()
    {
        this.textoDoNome = GetComponent<Text>();
    }

    public void AtualizaTexto(string Nome)
    {
        this.textoDoNome.text = Nome;
    }
}
