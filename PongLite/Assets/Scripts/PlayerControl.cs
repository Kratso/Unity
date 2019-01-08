using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector3 initialPosition;
    public float speed = 10f;

    // Use this for initialization
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            float y_ax = speed * Input.GetAxisRaw("Vertical") * Time.deltaTime;
            Vector3 mov = new Vector3(0, y_ax, 0);
            if ((transform.position.y < 3.85 && y_ax > 0) || (transform.position.y>-2.00 && y_ax < 0))
                transform.position += mov;
        }
    }

    public void Reset()
    {
        transform.position = initialPosition;
    }
}
