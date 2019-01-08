using UnityEngine;

public class Personaje : MonoBehaviour
{

    private int jumping;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-0.2f, 0.0f));
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(0.2f, 0.0f));
        }
        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && jumping < 2)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 500f));
            jumping++;
        }

        if (transform.position.y < -2.9)
            jumping = 0;
    }
}
