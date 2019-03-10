using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservaDeArmas : MonoBehaviour
{
    [SerializeField]
    private Arma[] reservaDeArmas;

    private Arma armaEquipada;
    private int indexDaArma = 0;

    private void Start()
    {
        this.armaEquipada = reservaDeArmas[indexDaArma];
    }

    public void EquiparArma(Arma arma)
    {
        this.armaEquipada = arma;
        arma.gameObject.SetActive(true);
    }

    public void TrocarArma()
    {
        if (indexDaArma == reservaDeArmas.Length-1)
        {
            armaEquipada.gameObject.SetActive(false);
            this.indexDaArma = 0;
            Arma novaArma = this.reservaDeArmas[indexDaArma];
            EquiparArma(novaArma);
        }

        else
        {
            armaEquipada.gameObject.SetActive(false);
            this.indexDaArma += 1;
            Arma novaArma = reservaDeArmas[indexDaArma];
            EquiparArma(novaArma);
        }
    }

    public void AtirarComAArmaEquipada()
    {
        armaEquipada.Atirar();
    }
}
