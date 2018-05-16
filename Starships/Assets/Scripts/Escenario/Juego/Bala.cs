using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bala : MonoBehaviour
{

    public float velocidad;
    public Rigidbody rbBala;

    private void Start()
    {
        rbBala = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        rbBala.velocity = transform.forward * velocidad;
    }

}
