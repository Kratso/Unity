using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            speed = Input.GetAxisRaw("vertical") * Time.deltaTime;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            speed = Input.GetAxisRaw("vertical") * Time.deltaTime;
	}
}
