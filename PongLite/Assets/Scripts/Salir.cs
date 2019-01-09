using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salir : MonoBehaviour {

    Scene scene;

    public Scene Menu;

    public Scene Juego;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        scene = SceneManager.GetActiveScene();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (scene.Equals(Menu)){
                Application.Quit();
            }
            else
            {
                SceneManager.SetActiveScene(Menu);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SceneManager.SetActiveScene(Juego);
        }
	}
}
