using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LP : MonoBehaviour {

    public PlayerControl pc;

    public BallMovement bmv;

    public GameObject gameOver;

    public Text livePoints;

    public Scene Menu;


    private int lp = 3;

	// Use this for initialization
	void Start () {
        gameOver.SetActive(false);
        livePoints.text = lp + "";
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.SetActiveScene(Menu);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        lp--;
        pc.Reset();
        bmv.Reset();
        livePoints.text = lp + "";

        if(lp == 0)
        {
            gameOver.SetActive(true);
        }
    }

    public bool IsAlive()
    {
        return lp > 0;
    }

    public void Reset()
    {
        lp = 3;
        bmv.Reset();
        pc.Reset();
        livePoints.text = lp + "";
        gameOver.SetActive(false);
    }

}
