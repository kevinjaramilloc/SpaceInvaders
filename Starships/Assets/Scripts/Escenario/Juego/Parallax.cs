using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public static Parallax parallax;

    [Header("Ciudad Y Nube")]
    public Transform[] BloqueCiudad;
    public Material materialNube;
    [SerializeField] private float velocidadCiuadad;
    [SerializeField] private float velocidadNube;
    [Header("Instancia Arma Dos")]
    public GameObject armaPoderosa;
    public Transform[] poscionesInst;
    [Header("Sonido")]
    public AudioSource audioFondo;
    int instLaser;
    int instPosiciones;


    private void Update()
    {

        if(GameManager.gameManager.utilNave == GameManager.utilidadesNave.volar)
        {
            /*Hacer que la ciudad se mueva y se ubique en una nueva posicion*/
            for(int i = 0; i < BloqueCiudad.Length; i++)
            {
                BloqueCiudad[i].Translate(-Vector3.forward *velocidadCiuadad * Time.deltaTime);

                if(BloqueCiudad[i].position.z < -67.815f)
                {
                    Vector3 newPosicion = BloqueCiudad[i].position;
                    newPosicion.z = 40f;
                    BloqueCiudad[i].position = newPosicion;
                }
            }
            /*Hacer que el material de la nube(Plano) cambie sus valores offset de "y" para dar la sensacion de movimiento */
            Vector2 newOffset = materialNube.mainTextureOffset;
            newOffset.x -= velocidadNube * Time.deltaTime;
            materialNube.mainTextureOffset = newOffset;          
        }
    }

    private void Start()
    {
        /*Llamando el metodo InvokeRepeating("InstanciaArmaPoderosa"), para que el objeto aparezca en un tiempo random de 10 a 30 segundos*/
        InvokeRepeating("InstanciaArmaPoderosa", Random.Range(10,20),Random.Range(20,30));
        audioFondo.Play();
    }

    /*Metodo para que aparezca en escena un objeto, el cual permitira que el jugador pueda disparar un arma que causa un daño mortal*/
    public void InstanciaArmaPoderosa()
    {
        if(GameManager.gameManager.utilNave == GameManager.utilidadesNave.volar)
        {
            instLaser = Random.Range(0, 1);
            instPosiciones = Random.Range(0, poscionesInst.Length);
            GameObject armaP = Instantiate(armaPoderosa, poscionesInst[instPosiciones].position, poscionesInst[instPosiciones].rotation);
            Destroy(armaP, 1f);
        }
    }

 
}
