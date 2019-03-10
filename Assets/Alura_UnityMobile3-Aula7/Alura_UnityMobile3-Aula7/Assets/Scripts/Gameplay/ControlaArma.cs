using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlaArma : MonoBehaviour
{
    [SerializeField]
    private UnityEvent TrocarArma; // Evento que chama o metodo de trocar a arma da reserva de armas

    [SerializeField]
    private UnityEvent Atirar; // Evento que chama o disparo da arma da reserva de armas

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TrocarArma.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Atirar.Invoke();
        }

        var toquesNaTela = Input.touches;
        foreach(var toque in toquesNaTela)
        {
            if(toque.phase == TouchPhase.Began)
            {
                Atirar.Invoke();
            }
        }
    }
}
