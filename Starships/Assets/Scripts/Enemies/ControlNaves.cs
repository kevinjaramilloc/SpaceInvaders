using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VaribalesControlNave : MonoBehaviour
{
    [Header("Enemigos Y Posiciones")]
    public GameObject[]                   Naves;
    public Transform[]                    poscionesInst;
}

public class ControlNaves : VaribalesControlNave
{

    int aparecer1;
    int aparecer2;
    bool verificaMetodos;

    private void Start()
    {
        verificaMetodos = false;

        /*invocando la funcion InvokeRepeating ("InstanciaEnemigosNormales")
         la cual me instancia enemigos de 1 a 5 segundos durante el transcurso del juego*/
        InvokeRepeating("InstanciaEnemigosNormales", Random.Range(1,4), Random.Range(1,6));
    }

    /*Metodo para enemigos normales, tienen una posicion desde donde apareceran*/
    void InstanciaEnemigosNormales()
    {
        
        if(GameManager.gameManager.utilNave == GameManager.utilidadesNave.volar && verificaMetodos == false)
        {
            /*instanciar enemigos en los puntos indicados*/
            verificaMetodos = false;
            aparecer1 = Random.Range(0, 3);
            aparecer2 = Random.Range(0, poscionesInst.Length);
            Instantiate(Naves[aparecer1], poscionesInst[aparecer2].position, poscionesInst[aparecer2].rotation);
        }
    }
}
