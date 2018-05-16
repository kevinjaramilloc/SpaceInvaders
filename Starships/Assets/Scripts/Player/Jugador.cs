using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class Variables : MonoBehaviour
{
    /*Variables llamadas desde la clase Jugador*/
    [Header("Nave")]
    public GameObject                       principalStarship;
    [Header("Salud")]
    public Image                            HealthyImage1;
    public Image                            HealthyImage2;
    public Image                            imagenSalud3;
    [Header("Movimiento")]
    public float                            velocidadNave;
    [Header("Disparo")]
    public GameObject                       armaLaser;
    public GameObject                       armaEspiral;
    public Transform                        transformLaser;
    public Transform                        transformArmaEspiral;
    public float                            SiguienteDisparo;
    public float                            velocidadLaser;
    [Header("Particulas")]
    public GameObject                       explosionMorir;
    public GameObject                       efectoDañoNave;
    [Header("Sonido")]
    public AudioSource                      audioDisparo;
    public AudioSource                      audioArmaEspiral;
    public AudioSource                      audioExplosion;
}

public class Jugador : Variables
{
    public static Jugador                  jugador;
    private Rigidbody                      rb;
    GameObject                             bala;

    /*Inicializando componentes*/
    private void Awake()
    {
        /*Singleton*/
        if(!jugador)
        {
            jugador = this;
        }
        else
        {
            Destroy(this);
        }
        /*Inicializando componente Rigidbody*/
        rb = GetComponent<Rigidbody>();
    }

    /*Funcion de disparo y verificaciones de la salud de la nave*/
    private void Update()
    {
        /*Metodo para disparar*/
        Disparar();

        /*Si la slaud del jugador llega a cero se activara la particula de explosion, se destruira el personaje y se cargara la escena de game over*/
        if(imagenSalud3.fillAmount <= 0)
        {
            GameObject parti = Instantiate(explosionMorir, transform.position, Quaternion.identity);
            audioExplosion.Play();
            Destroy(principalStarship,0.2f);
            Destroy(parti,1f);
        }

        if(HealthyImage1.fillAmount <= 0)
        {
            Destroy(HealthyImage1);
        }

        if(HealthyImage2.fillAmount <= 0)
        {
            Destroy(HealthyImage2);
        }
    }

    /*Llamando metodo de movimiento de la nave*/
    public void FixedUpdate()
    {
        AplicarMovimiento();
    }

    private void OnTriggerEnter(Collider other)
    {
        /*Si el personaje colisiona con las balas del enemigo su salud se reducira*/
            if(HealthyImage1 && other.gameObject.tag == "BalaEnemigo")
            {
                HealthyImage1.fillAmount -= 0.02f;
            }
            if(!HealthyImage1 && other.gameObject.tag == "BalaEnemigo")
            {
                /*Activar efecto de daño par la nave principal*/
                efectoDañoNave.SetActive(true);
                HealthyImage2.fillAmount -= 0.02f;
            }
            if(!HealthyImage2 && other.gameObject.tag == "BalaEnemigo")
            {
                imagenSalud3.fillAmount -= 0.02f;
            }

            if(HealthyImage1 && other.gameObject.tag == "BalaLaser")
            {
                HealthyImage1.fillAmount -= 0.1f;
            }
            if(!HealthyImage1 && other.gameObject.tag == "BalaLaser")
            {
                HealthyImage2.fillAmount -= 0.1f;
            }
            if(!HealthyImage2 && other.gameObject.tag == "BalaLaser")
            {
                imagenSalud3.fillAmount -= 0.1f;
            }
            /*si el personaje colisiona con un tag llamado enemigo, los dos se destruiran y se activa la escena de game over*/
        if(other.gameObject.tag == "Enemigo")
        {
            GameObject parti = Instantiate(explosionMorir, transform.position, Quaternion.identity);
            audioExplosion.Play();
            Destroy(principalStarship, 0.2f);
            Destroy(parti, 1.5f);
        }

        /*si el personaje colisiona con un tag nombrado armaespiral se instanciara un arma que abarca el ancho de la pantalla
         la cual cuasa daño inmediato a los enemigos normales*/
        if(other.gameObject.tag == "RecogerArmaEspiral")
        {
            GameObject bala2 = Instantiate(armaEspiral, transformArmaEspiral.position, Quaternion.identity);
            Destroy(bala2, 10f);
            audioArmaEspiral.Play();
        }
    }

    /*Control de movimiento de la nave*/
    void AplicarMovimiento()
    {
        if(GameManager.gameManager.utilNave == GameManager.utilidadesNave.volar)
        {
            float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");
            Vector3 movimiento = new Vector3(moveHorizontal, 0, moveVertical);
            rb.velocity = movimiento * velocidadNave;
            /*especificando posiciones hasta donde puede ir, para que la nave no se salga de la pantalla
             * el mathf me sujeta un valor entre dos flotantes uni¿o minimo y otro maximo*/
            rb.position = new Vector3(Mathf.Clamp(rb.position.x, -4.8f, 4.8f), 0f, Mathf.Clamp(rb.position.z, -6.5f, 6.5f));
        }
    }

    /*Contorl de disparo de la nave*/
    public void Disparar()
    {
        /* Al presionar el click izquierdo del mouse se instanciaran las balas
         * y se le asignara el tiempo en el cual puede volver a disparar
         * al pasar 5 segundos las instancias de las balas se destruiran*/
        if(CrossPlatformInputManager.GetButton("Fire1") && Time.time > SiguienteDisparo)
        {
            SiguienteDisparo = Time.time + velocidadLaser;
            bala = Instantiate(armaLaser, transformLaser.position, Quaternion.identity);
            audioDisparo.Play();
            Destroy(bala,5f);
        }
    }
}

