using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Status : MonoBehaviour, ICuravel, IMatavel
{

    [SerializeField]
    private UnityEvent Morreu;

    public int Vida;

    public int VidaInicial = 100;
    public float Velocidade = 5;

    void Awake ()
    {
        this.Vida = this.VidaInicial;
    }

    public void PersonagemMorreu()
    {
        if(this.Vida <= 0)
        {
            Morreu.Invoke();
        }
    }

    public void CurarVida(int cura)
    {
        this.Vida += cura;
        if (this.Vida > this.VidaInicial)
        {
            this.Vida = this.VidaInicial;
        }
    }

    public void TomarDano(int dano)
    {
        this.Vida -= dano;
        Morrer();
    }

    public void Morrer()
    {
        if (Vida <= 0)
        {
            Morreu.Invoke();
        }
    }
}
