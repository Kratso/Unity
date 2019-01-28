using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointManager : MonoBehaviour {
	public Text cuentaPuntos;//Variable para guardar el texto referente a los puntos del jugador en la partida.
	public Text puntu;//Variable para guardar el texto referente a los puntos del jugador en la partida, pero en el menú.
	public Text puntumax;//Variable para guardar el texto referente a los puntos máximos del jugador en el menú.
	public GameObject menu;//Variable para guardar el menú.
	//Variables para activar las opciones del menú.
	public ElementoInteractivo salir;
	public ElementoInteractivo repetir;
	//Variables para guardar los puntos.
	int record;
	int puntos=0;
	//Audio interactivo.
	public AudioSource perder;

	void Start(){
		//Primero obtenemos el record.
		record = PlayerPrefs.GetInt ("record");
		//Inicializamos los puntos.
		cuentaPuntos.text = "Puntos: " + puntos;
	}

	void Update(){
		//Acciones según lo escogido en el menú.
		if (salir.pulsado) {
			Application.Quit();
		}
		if (repetir.pulsado) {
			SceneManager.LoadScene ("Principal");
		}
	}

	//Método que suma 1 punto si lo llama una baldosa.
	public void baldosa(){
		puntos = puntos + 1;
		cuentaPuntos.text = "Puntos: " + puntos;
	}

	//Método que suma 10 puntos si lo llama un diamante.
	public void diamante(){
		puntos = puntos + 10;
		cuentaPuntos.text = "Puntos: " + puntos;
	}

	//Método para el final de la partida.
	public void endGame(){
		perder.Play ();//Llama al audio.
		menu.SetActive (true);//Muestra el menú.
		puntu.text = "" + puntos;//Muestra los puntos finales.
		//Sobrescribe el record si se superado.
		if (record < puntos) {
			record = puntos;
			PlayerPrefs.SetInt ("record",record);
		}
		puntumax.text = "" + record;//Muestra el record de puntos.
	}
}
