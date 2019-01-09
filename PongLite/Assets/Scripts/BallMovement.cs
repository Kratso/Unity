using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallMovement : MonoBehaviour {

    public float startingSpd = 400f;

    public Rigidbody rig;

    public Transform player;

    bool inGame = false;

    public LP lp;

    Vector3 startingPos;

	// Use this for initialization
	void Start () {

        startingPos = transform.position;


    }
	
	public void Reset()
    {
        inGame = false;
        transform.SetParent(player);
        transform.position = startingPos;
        rig.velocity = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        if (lp.IsAlive())
        {
            if (!inGame)
        {
            rig.velocity = Vector3.zero;
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                inGame = true;
                transform.SetParent(null);
                rig.AddForce(new Vector3(-startingSpd, -startingSpd, 0));
                transform.position = startingPos;
            }
        } 
        else
        {
                float rng = Random.value * 10;

                rng -= 5;

                float rng2 = Random.value;

                if (rng2 < 0.1f)
                {
                    rig.AddForce(rng, rng, 0);
                }
            }
        if (Input.GetKeyDown(KeyCode.R)){
            Reset();
        }

        }
    }


}
