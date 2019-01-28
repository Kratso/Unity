using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocaDiamante : MonoBehaviour {
	public GameObject particulas;//Variable para guardar el prefab para el efecto "particulas".
	GameObject accion;//Variable para instanciar el objeto "particulas".
	public GameObject diamond;//Variable para guardar el diamante.
	public PointManager pm;//Administrador de juego.
	public AudioSource tocar;//Audio interactivo.


	//Si se activa el trigger del colider...
	void OnTriggerEnter (Collider col){
		//Solo hará algo si lo que lo activa es un objeto "Jugador".
		if (col.name == "Jugador") {
			tocar.Play ();//Llama al audio.
			pm.diamante ();//Suma puntos por el diamante.
			diamond.SetActive (false);//Oculta/desactiva el diamante.
			//Instancia las "particulas".
			accion = Instantiate (particulas, 
				col.gameObject.transform.position,
				Quaternion.identity);
			//Destruye las "particulas" cuando dejen de ser útiles.
			Destroy (accion, 5);
		}
	}
}
