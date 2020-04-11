using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Rigidbody2D rb2;
    public float wantedRotation1;
    public float wantedRotation2;
    public bool scanright=true;

    int waypointID = 0;
    bool playerspotted=false;
    public float scanspeed;
    public float movespeed;
    Transform player;

    public float pacespeed;

    //enemy circles around waypoints
    public Vector2[] waypoints;
    public float[] rotationatwaypoint;

    public bool scanning=true;
    public bool pacing = true;

   public float RotationSpeed;


    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (scanning)
        {
            Scan();
        }
        if (pacing)
        {
            pace();
        }
        if (playerspotted)
        {
            scanning = false;
            pacing = false;
            chase();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("enemy alerted");
            playerspotted = true;
            player = col.gameObject.transform;
        }
    }

    //Rotates in search of intruders
    void Scan()
    {

        if (!scanright)
        {
            Debug.Log(rb2.rotation);
            rb2.MoveRotation(rb2.rotation + RotationSpeed * Time.fixedDeltaTime);
            if (rb2.rotation == wantedRotation1)
            {
                scanright = true;
            }
        }
        else
        {
            rb2.MoveRotation(rb2.rotation - RotationSpeed * Time.fixedDeltaTime);

            if (rb2.rotation == wantedRotation2)
            {
                scanright = false;
            }
        }
        

    }

    void chase()
    {
        //follow player
        Vector2 movePosition = transform.position;

        movePosition.x = Mathf.MoveTowards(transform.position.x, player.position.x, movespeed * Time.deltaTime);
        movePosition.y = Mathf.MoveTowards(transform.position.y, player.position.y, movespeed * Time.deltaTime);

        rb2.MovePosition(movePosition);


        //look at player
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        Quaternion wantedRotation = Quaternion.AngleAxis(angle, -Vector3.forward);
        transform.rotation = wantedRotation;
    }

    //Walk to waypoints
    void pace()
    {

        Vector2 currentwaypoint=waypoints[waypointID];
        Vector2 movePosition = transform.position;

        movePosition.x = Mathf.MoveTowards(transform.position.x, currentwaypoint.x, pacespeed * Time.deltaTime);
        movePosition.y = Mathf.MoveTowards(transform.position.y, currentwaypoint.y, pacespeed * Time.deltaTime);

        rb2.MovePosition(movePosition);
        

        if (movePosition == currentwaypoint)
        {
            rb2.rotation = rotationatwaypoint[waypointID];
            if (waypoints.Length - 1 > waypointID)
            {
                waypointID = waypointID + 1;
            }
            else
            {
                waypointID = 0;
            }
        }
    }
}
