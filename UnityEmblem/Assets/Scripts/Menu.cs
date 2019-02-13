using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

	public Scene scene;

	void Awake()
	{
		scene = SceneManager.GetActiveScene();
	}

	public void onClickStart(int index)
	{
		SceneManager.LoadScene(index);
	}

	public void onClickExit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
	}
}

