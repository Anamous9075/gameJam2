using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public Rigidbody2D rb2;
    public float speed = 3f;
    private void Update()
    {
        Move();
    }

    public void Move()
    {

        if (Input.GetKey(KeyCode.W))
        {
            rb2.MovePosition(rb2.position + Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb2.MovePosition(rb2.position + Vector2.down * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb2.MovePosition(rb2.position + Vector2.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb2.MovePosition(rb2.position + Vector2.left * speed * Time.deltaTime);

        }
    }


}
