using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemigoNormal : ClaseEnemigo
{
    [Header("Caracteristica Enemigo")]
    public GameObject                   Enemigo;
    public float                        moveSpeed;
    public float                        daño;
    [Header("Disparo")]
    public GameObject                   laser;
    public Transform                    transformLaser;
    float                               SiguienteDisparo;
    float                               velocidadLaser;
    [Header("Particulas")]
    public GameObject                   explosionMorir;
    [Header("Materiales")]
    public Material[]                   materialNaves;
    [Header("Sonido")]
    public AudioSource                  audioDisparo;

    public override void RecibirDaño(float daño)
    { 
        vida -= daño;
    }

    private void Start()
    {
        /*Asignando un material random al enemigo*/
        GetComponentInChildren<Renderer>().material = materialNaves[UnityEngine.Random.Range(0, materialNaves.Length)];
        /*Invoco el disparo del enemigo cada 1 segundo*/
        InvokeRepeating("Disparar", 0.5f, 0.5f);
    }

    private void Update()
    {
        /*Movimiento del enemigo*/
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    /*En el trigger le digo al enemigo normal que puede hacerle daño, y que sucedera si su salud se reduce por completo*/
    private void OnTriggerEnter(Collider other)
    {
        /*Si el laser del personaje toca al enemigo le reducira la vida (La cual se indica desde el inspector)*/
        if(other.gameObject.tag == "Laser")
        {
            vida -= daño;
            /*Si la vida llega a cero se destruira el enemgio*/
            if(vida <= 0)
            {
                GameObject parti = Instantiate(explosionMorir, transform.position, Quaternion.identity);
                Destroy(Enemigo, 0.1f);
                Destroy(parti, 1f);
            }
        }

        /*Si el enemigo colisiona con el impacto del arma espiral se destruira de inmediato.
         *si el enemigo choca con otro enemigo se destruiran los dos.
         *si el enemigo choca con el jugador se destruiran los dos*/
        if(other.gameObject.tag == "ArmaEspiral" || other.gameObject.tag == "Enemigo")
        {
            GameObject parti = Instantiate(explosionMorir, transform.position, Quaternion.identity);
            Destroy(Enemigo, 0.1f);
            Destroy(parti, 1f);
        }
        /*Si la nave sale de la pantalla estara una pared en la cual se destruiran las instancias*/
        if(other.gameObject.tag == "pared")
        {
            Destroy(Enemigo);
        }
    }

    /*Disparo del enemigo*/
    void Disparar()
    {
        SiguienteDisparo = Time.time + velocidadLaser;
        /*instancia de la bala del enemigo normal*/
        GameObject bala = Instantiate(laser, transformLaser.position, Quaternion.Euler(0,180,0));
        audioDisparo.Play();
        Destroy(bala,5f);
    }
}