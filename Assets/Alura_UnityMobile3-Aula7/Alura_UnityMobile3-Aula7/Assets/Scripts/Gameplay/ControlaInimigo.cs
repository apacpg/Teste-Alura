using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Forca a adicao dos scrips no zumbi instanciado
[RequireComponent(typeof(MovimentoPersonagem))]
[RequireComponent(typeof(AnimacaoPersonagem))]
[RequireComponent(typeof(Status))]
[RequireComponent(typeof(Interfaces))]
public class ControlaInimigo : MonoBehaviour, IMatavel, IReservavel
{

    private GameObject Jogador;
    private MovimentoPersonagem movimentaInimigo;
    private Status statusInimigo;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private ControlaInterface scriptControlaInterface;
    private IReservaDeObjetos reserva;
    private AudioSource audio;

    [SerializeField]
    private LevaDano SofrerDano; // Evento que tira vida do inimigo

    [SerializeField]
    private AnimarAtaque AnimacaoDeAtaque; // Evento que chama a animacao de ataque do inimigo

    [SerializeField]
    private AnimarMovimento AnimacaoDeMovimento; // Evento que chama a animacao de movimento do inimigo

    [SerializeField]
    private UnityEvent AnimacaoDeMorte; // Evento que chama a animacao de morte do inimigo

    [SerializeField]
    private UnityEvent AtualizaNumeroDeZumbiesMortos; // Evento que atualiza na interface o numero de zumbis mortos

    [SerializeField]
    private float porcentagemGerarKitMedico = 0.1f;

    [HideInInspector]
    public GeradorZumbis meuGerador;

    [Range(10, 30)]
    public int danoMinimo = 20; // Delimita uma faixa de dano minimo que o inimigo pode causar

    [Range(30, 40)]
    public int danoMaximo = 30; // Delimita uma faixa de dano maximo que o inimigo pode causar

    [HideInInspector]
    public GameObject ParticulaSangueZumbi;
    public AudioClip SomDeMorte;
    [HideInInspector]
    public AudioClip SomDeGerarKitMedico;
    [HideInInspector]
    public GameObject KitMedicoPrefab;
    public float tempoEntrePosicoesAleatorias = 4;

    public void SetReserva(IReservaDeObjetos reserva)
    {
        this.reserva = reserva;
    }
    private void Awake()
    {
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
    }

    void Start () {
        Jogador = GameObject.FindWithTag("Jogador");
        audio = GameObject.FindGameObjectWithTag("SFXAudio").GetComponent<AudioSource>();
        statusInimigo = GetComponent<Status>();
        AleatorizarZumbi();
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        movimentaInimigo.Rotacionar(direcao);
        AnimacaoDeMovimento.Invoke(direcao.magnitude);
        //animacaoInimigo.Movimentar(direcao.magnitude);

        if(distancia > 15)
        {
            Vagar ();
        }
        else if (distancia > 2.5)
        {
            direcao = Jogador.transform.position - transform.position;
            movimentaInimigo.SetDirecao(direcao);
            movimentaInimigo.Movimentar(statusInimigo.Velocidade);

            AnimacaoDeAtaque.Invoke(false);
            //animacaoInimigo.Atacar(false);
        }
        else
        {
            direcao = Jogador.transform.position - transform.position;

            AnimacaoDeAtaque.Invoke(true);
            //animacaoInimigo.Atacar(true);
        }
    }

    void Vagar ()
    {
        contadorVagar -= Time.deltaTime;
        if(contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorias + UnityEngine.Random.Range(-1f, 1f);
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;
        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.SetDirecao(direcao);
            movimentaInimigo.Movimentar(statusInimigo.Velocidade);
        }           
    }

    Vector3 AleatorizarPosicao ()
    {
        Vector3 posicao = UnityEngine.Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    void AtacaJogador ()
    {
        int dano = UnityEngine.Random.Range(danoMinimo, danoMaximo);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void AleatorizarZumbi ()
    {
        int geraTipoZumbi = UnityEngine.Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        SofrerDano.Invoke(dano);
    }

    public void ParticulaSangue (Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(ParticulaSangueZumbi, posicao, rotacao);
    }

    public void Morrer()
    {
        Invoke("VoltarParaReserva", 2);
        AnimacaoDeMorte.Invoke();
        //animacaoInimigo.Morrer();
        movimentaInimigo.Morrer();
        this.enabled = false;
        audio.PlayOneShot(SomDeMorte);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        //scriptControlaInterface.AtualizarQuantidadeDeZumbisMortos();
        AtualizaNumeroDeZumbiesMortos.Invoke();
        
    }

    private void VoltarParaReserva()
    {
        this.reserva.DevolverObjeto(this.gameObject);
    }

    void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        if (UnityEngine.Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
            audio.PlayOneShot(SomDeGerarKitMedico);
        }
    }

    public void AoEntrarNaReserva()
    {
        this.movimentaInimigo.Reiniciar();
        this.enabled = true;
        this.gameObject.SetActive(false);
    }

    public void AoSairDaReserva()
    {
        this.gameObject.SetActive(true);
    }
}

#region Eventos

[Serializable]
public class LevaDano : UnityEvent<int>{ }

[Serializable]
public class AnimarAtaque : UnityEvent<bool> { }

[Serializable]
public class AnimarMovimento : UnityEvent<float> { }

#endregion