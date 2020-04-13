using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public Rigidbody2D rb2;
    public float speed = 3f;

    private void Start()
    {
        Physics.gravity = new Vector3(0, 0, 0);
    }


    private void Update()
    {
        Move();
        mouseaim();
    }

    public void Move()
    {

        float _xMove = Input.GetAxisRaw("Horizontal") * speed;
        float _yMove = Input.GetAxisRaw("Vertical") * speed;

        rb2.MovePosition(rb2.position + new Vector2(_xMove * Time.deltaTime, _yMove * Time.deltaTime));
    }

    public void mouseaim()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(rb2.position);
        var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        rb2.rotation = -angle;
    }
}
