using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arma : MonoBehaviour // Determina as propriedades de uma arma
{
    public GameObject CanoDaArma; // posicao de instanciacao do tiro
    public AudioClip SomDoTiro; // som do tiro
    public float frequenciaDeTiro = 1f; // frequencia de disparo
    public ReservaExtensivel reservaDeBalas; // reserva de balas

    [SerializeField]
    private SomDeDisparo FazSomDeDisparo; // Evento que reproduz o som do disparo

    private float contadorDoUltimoTiro = 0; // Contador que verifica se pode ocorrer um disparo

    private void Update()
    {
        contadorDoUltimoTiro += Time.deltaTime;
    }

    public void Atirar() // Atira uma bala
    {
        if (PodeAtirar())
        {
            if (this.reservaDeBalas.TemObjeto())
            {
                var bala = this.reservaDeBalas.PegarObjeto();
                bala.transform.position = CanoDaArma.transform.position;
                bala.transform.rotation = CanoDaArma.transform.rotation;
                FazSomDeDisparo.Invoke(SomDoTiro);
                contadorDoUltimoTiro = 0;
            }
        }
    }

    private bool PodeAtirar() // verifica se e possivel atirar
    {
        if (contadorDoUltimoTiro >= frequenciaDeTiro)
            return true;

        return false;
    }
}

[Serializable]
public class SomDeDisparo : UnityEvent<AudioClip> { }