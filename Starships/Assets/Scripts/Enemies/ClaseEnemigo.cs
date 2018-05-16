using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClaseEnemigo : MonoBehaviour
{

    public float vida;
    public abstract void RecibirDaño(float daño);
}
