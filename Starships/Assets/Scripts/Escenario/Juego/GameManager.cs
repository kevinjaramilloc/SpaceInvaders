using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    public utilidadesNave utilNave;

    public enum utilidadesNave
    {
        volar,morir
    }

    private void Awake()
    {
        if(!gameManager)
        {
            gameManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    /*Modificar estado del enum*/
    public void ChangeState(utilidadesNave newUtilidades)
    {
        utilNave = newUtilidades;
    }


}
