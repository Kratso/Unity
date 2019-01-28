using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class DetectaBola : MonoBehaviour
{
    public TyleManager tm;//Variable administradora de baldosas.
    public PointManager pm;//Variable administradora de juego.
    public GameObject baldosa;//Variable para guardar nuestra baldosa.
    public Rigidbody rig;//Variable para guardar el Rigidbody de nuestra baldosa.

    public float fovMax = 100f;

    public Camera cam;

    // Use this for initialization
    void Start()
    {
		cam = GameObject.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Si se activa el trigger del colider...
    void OnTriggerExit(Collider other)
    {
        //Solo hará algo si lo que lo activa es un objeto "Jugador".
        if (other.name == "Jugador")
        {
            pm.baldosa();//Cuenta el punto de la baldosa.
            tm.crearBaldosa();//Crea una baldosa.
            StartCoroutine(tm.destruirBaldosa(baldosa, rig));//Destruye la baldosa actual cuando deje de ser útil.
            MoverJugador mj = GameObject.FindObjectOfType<MoverJugador>();
            mj.velocidad += 0.1f;
            if (cam.fieldOfView < fovMax)
            {
                cam.fieldOfView += .5f;
            }
        }

    }
}
