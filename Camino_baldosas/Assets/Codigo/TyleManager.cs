using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class TyleManager : MonoBehaviour {
	//Variables para guardar baldosas.
	public GameObject currenTile;
	public GameObject miprefab;
	private static TyleManager instance;
	//Variable para la inicialización del número de baldosas.
	int cont = 0;
	//Variable para la generación/activación semi-aleatoria de diamantes.
	int i = 0;

	private static TyleManager Instance{
		get{
			if (instance == null) {
				instance = GameObject.FindObjectOfType<TyleManager> ();
			}
			return instance;
		}
	}


	// Use this for initialization
	void Start () {
		iniciarBaldosas ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	//Crea una copia de la baldosa actual.
	public void crearBaldosa(){
		//Genera un entero entre 0 y 1.
		int opcion = Random.Range(0, 2);
		//Si es 0 crea la baldosa a la izquierda.
		if (opcion == 0) {
			//Instancia la baldosa.
			currenTile = (GameObject)Instantiate (miprefab,
			new Vector3 (miprefab.transform.position.x - 5, miprefab.transform.position.y, miprefab.transform.position.z), Quaternion.identity);
			//Igualamos a 'false' o 'true' el objeto hijo de nuestro currenTile.
			if (i == 8 || i==1) {
				currenTile.transform.GetChild (1).gameObject.SetActive(true);
			}else {
				currenTile.transform.GetChild (1).gameObject.SetActive (false);
			}
			//Guarda como "última baldosa" a la que acabamos de crear.
			miprefab = currenTile;
		} else {
			//Instancia la baldosa.
			currenTile = (GameObject)Instantiate (miprefab,
			new Vector3 (miprefab.transform.position.x, miprefab.transform.position.y, miprefab.transform.position.z+5), Quaternion.identity);
			//Igualamos a 'false' o 'true' el objeto hijo de nuestro currenTile.
			if (i == 10 || i == 3) {
				currenTile.transform.GetChild (1).gameObject.SetActive (true);
			} else {
				currenTile.transform.GetChild (1).gameObject.SetActive (false);
			}
			//Guarda como "última baldosa" a la que acabamos de crear.
			miprefab = currenTile;
		}
		i++;
		if (i == 10) {
			i = 0;
		}
	}

	//Método que destruye una baldosa cada 'x' segundos.
	public IEnumerator destruirBaldosa(GameObject o, Rigidbody r){
		//La baldosa se cae.
		yield return new WaitForSeconds((float)0.4);
		r.isKinematic = false;
		//La baldosa se destruye.
		yield return new WaitForSeconds((float)0.75);
		Destroy(o);
	}

	//Método que inicia las baldosas.
	public void iniciarBaldosas(){
		while (cont < 10) {
			crearBaldosa ();
			cont++;
		}
	}
}
