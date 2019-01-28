using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoverJugador : MonoBehaviour {
	bool iniciado=false;//Variable para iniciar el juego.
	int direccion=0;//Variable para guardar la dirección que coge la bola.
	public PointManager pm;//Administrador de juego(puntos).
	bool fin=false;//Variable para terminar el juego.
	public float velocidad = 10f;


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		/*
		 * En cuanto se pulse click izquierdo se empezará a mover la bola.
		 * Luego, cada vez que se vuelva a pulsar click, cambiará de dirección.
		 */
		if (Input.GetMouseButtonDown (0)) {
			iniciado = true;
			if (direccion == 1) {
				direccion = 0;
			} else {
				direccion = 1;
			}
		}
		//Si el juego está iniciado, cambia la dirección de la bola, que puede ser hacia delante o izquierda.
		if (iniciado) {
			if (!fin) {
				if (direccion == 1) {
					transform.Translate (Vector3.forward * velocidad * Time.deltaTime);
				} else {
					transform.Translate (Vector3.left * velocidad * Time.deltaTime);
				}
				if (transform.position.y < 3) {
					endGame ();
				}
			}
		}
	}

	//Método que separa la bola de la cámara y la luz y, mediante el administrador de juego, finaliza la partida.
	public void endGame(){
		//Separa bola y luz.
		transform.GetChild (1).transform.parent = null;
		transform.GetChild (0).transform.parent = null;
		//Finaliza juego.
		fin = true;
		pm.endGame ();
	}
}
