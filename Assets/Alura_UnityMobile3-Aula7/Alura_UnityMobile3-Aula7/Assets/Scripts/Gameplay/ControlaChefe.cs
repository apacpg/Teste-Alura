using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;


// Forca a adicao dos scripts abaixo
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Status))]
[RequireComponent(typeof(MovimentoPersonagem))]
public class ControlaChefe : MonoBehaviour, IMatavel, IReservavel
{
    private Transform jogador;
    private NavMeshAgent agente;
    private Status statusChefe;
    private MovimentoPersonagem movimentoChefe;
    private IReservaDeObjetos reserva;
    private AudioSource audio;

    [HideInInspector]
    public Slider sliderVidaChefe;
    [HideInInspector]
    public Image ImagelSlider;
    [HideInInspector]
    public Color CorDaVidaMaxima, CorDaVidaMinima;
    [HideInInspector]
    public GameObject ParticulaSangueZumbi;
    [HideInInspector]
    public GameObject KitMedicoPrefab;

    [SerializeField]
    private UnityEvent AnimacaoDeMorteDoChefe; // Evento que chama a animacao de morte do chefe

    [SerializeField]
    private LevarDanoDoChefe DanoNoChefe; // Evento que danifica o chefe

    [SerializeField]
    private AnimarAtaqueDoChefe AnimacaoDeAtaque; // Evento que chama a animacao de ataque do chefe
 
    [SerializeField]
    private AnimarMovimentoDoChefe AnimacaoDeMovimentoDoChefe; // Evento que chama a animacao de movimento do chefe

    [SerializeField]
    private UnityEvent AtualizarPontuacao; // Evento que atualiza a pontuacao quando o chefe morre

    public AudioClip SomDeAtaque;

    [HideInInspector]
    public AudioClip SomDeDerrubarKitDeVida;

    [Range(25, 35)]
    public int danoMinimo = 30;
    [Range(35, 45)]
    public int danoMaximo = 40;

    private void Awake()
    {
        movimentoChefe = GetComponent<MovimentoPersonagem>();
        agente = GetComponent<NavMeshAgent>();
        statusChefe = GetComponent<Status>();
    }

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador").transform;
        audio = GameObject.FindGameObjectWithTag("SFXAudio").GetComponent<AudioSource>();
        agente.speed = statusChefe.Velocidade;
        sliderVidaChefe.maxValue = statusChefe.VidaInicial;
        AtualizarInterface();
    }

    public void SetPosicao(Vector3 posicao)
    {
        this.transform.position = posicao;
        this.agente.Warp(posicao);
    }

    private void Update()
    {
        agente.SetDestination(jogador.position);
        AnimacaoDeMovimentoDoChefe.Invoke(agente.velocity.magnitude);
        //animacaoChefe.Movimentar(agente.velocity.magnitude);

        if (agente.hasPath == true)
        {
            bool estouPertoDoJogador = agente.remainingDistance <= agente.stoppingDistance;

            if (estouPertoDoJogador)
            {
                AnimacaoDeAtaque.Invoke(true);
                //animacaoChefe.Atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                movimentoChefe.Rotacionar(direcao);
            }
            else
            {
                AnimacaoDeAtaque.Invoke(false);
                //animacaoChefe.Atacar(false);
            }
        }
    }

    void AtacaJogador ()
    {
        int dano = UnityEngine.Random.Range(danoMinimo, danoMaximo);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
        audio.PlayOneShot(SomDeAtaque);
    }

    public void TomarDano(int dano)
    {
        DanoNoChefe.Invoke(dano);
        //statusChefe.Vida -= dano;
        AtualizarInterface();
        //if (statusChefe.Vida <= 0)
        //{
        //    Morrer();
        //}
    }

    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(ParticulaSangueZumbi, posicao, rotacao);
    }

    public void Morrer()
    {
        AnimacaoDeMorteDoChefe.Invoke();
        //animacaoChefe.Morrer();
        movimentoChefe.Morrer();
        this.enabled = false;
        agente.enabled = false;
        Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        audio.PlayOneShot(SomDeDerrubarKitDeVida);
        Invoke("VoltarParaReserva", 2);
        AtualizarPontuacao.Invoke();
    }

    private void VoltarParaReserva()
    {
        this.reserva.DevolverObjeto(this.gameObject);
    }

    private void AtualizarInterface ()
    {
        sliderVidaChefe.value = statusChefe.Vida;
        float porcentagemDaVida = (float)statusChefe.Vida / statusChefe.VidaInicial;
        Color corDaVida = Color.Lerp(CorDaVidaMinima, CorDaVidaMaxima, porcentagemDaVida);
        ImagelSlider.color = corDaVida;
    }

    public void SetReserva(IReservaDeObjetos reserva)
    {
        this.reserva = reserva;
    }

    public void AoEntrarNaReserva()
    {
        this.gameObject.SetActive(false);
        this.movimentoChefe.Reiniciar();
        this.enabled = true;
        agente.enabled = true;
        statusChefe.Vida = statusChefe.VidaInicial;
    }

    public void AoSairDaReserva()
    {
        this.gameObject.SetActive(true);
    }
}

#region Eventos

[Serializable]
public class LevarDanoDoChefe : UnityEvent<int> { }

[Serializable]
public class AnimarAtaqueDoChefe : UnityEvent<bool> { }

[Serializable]
public class AnimarMovimentoDoChefe : UnityEvent<float> { }

#endregion