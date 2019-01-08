using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    public float startingSpd = 400f;

    public Rigidbody rig;

    public Transform player;

    bool inGame = false;

    Vector3 startingPos;

	// Use this for initialization
	void Start () {
        


	
	}
	
	void Reset()
    {
        inGame = false;
        transform.SetParent(player);
        transform.position = startingPos;
        rig.velocity = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        if (!inGame)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                inGame = true;
                transform.SetParent(null);
                rig.AddForce(new Vector3(startingSpd, startingSpd, 0));
                startingPos = transform.position;
            }
        }
		if(Input.GetKeyDown(KeyCode.R)
		{
			Reset();
		}
	}

    
}
