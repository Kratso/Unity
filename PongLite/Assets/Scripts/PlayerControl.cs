using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            float y_ax = speed * Input.GetAxisRaw("Vertical") * Time.deltaTime;
            Vector3 mov = new Vector3(0, y_ax, 0);
            if ((transform.position.y < 3.85 && y_ax > 0) || (transform.position.y>-1.96 && y_ax < 0))
                transform.position += mov;
        }
    }

    private void Reset()
    {
        transform.position.Set(transform.position.x,0.66f,transform.position.z);
    }
}
