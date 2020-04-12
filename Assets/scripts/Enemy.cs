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
    Vector2 movePosition;
    //For scanning (not implemented fully
    public float RotationSpeed;

    //for shooting if player is found
    public Transform firepoint;
    public float bulletforce;
    public float turntoplayerspeed;
    public GameObject bullet;

    public float firingspeed;
    float fireinterval;
    // Start is called before the first frame update

    public float wanderinterval;
    public bool wanderer;
    Vector2 targetpos;

   void  Start()
    {
       targetpos = rb2.position;
        movePosition =transform.position;
    }

    public float wanderRate;
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
            wanderer = false;
            chase();
        }
        if (wanderer)
        {
            Wander();
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

            var dir = new Vector2(player.position.x,player.position.y) - rb2.position;
            var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            rb2.MoveRotation(angle);

        //shoot at player

        if (fireinterval < Time.time)
        {
            Shoot();
            fireinterval = Time.time + firingspeed;
        }


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

    void Shoot()
    {
        Instantiate(bullet, firepoint.position, firepoint.rotation);
        Rigidbody2D Bulletrb2 = bullet.GetComponent<Rigidbody2D>();
        Bulletrb2.AddForce(firepoint.up * bulletforce, ForceMode2D.Impulse);
    }

    //Move in one of 8 directions every few seconds with a 1/3 chance of staying still
    void Wander()
    {


        int wandervalue=Random.Range(1, 12);

        if (wanderinterval < Time.time)
        {

            if (wandervalue == 1)
            {
                targetpos.x = rb2.position.x - 2;
                rb2.rotation = -90;
            }
            else if (wandervalue == 2)
            {
                targetpos.x = rb2.position.x + 2;
                rb2.rotation = 90;
            }
            else if (wandervalue == 3)
            {
                targetpos.y = rb2.position.y - 2;
                rb2.rotation = 180;
            }
            else if (wandervalue == 4)
            {
                targetpos.y = rb2.position.y + 2;
                rb2.rotation = 0;
            }
            else if (wandervalue == 5)
            {
                targetpos.x = rb2.position.x - 2;
                targetpos.y = rb2.position.y + 2;
                rb2.rotation = 135;

            }
            else if (wandervalue == 6)
            {
                targetpos.x = rb2.position.x + 2;
                targetpos.y = rb2.position.y + 2;
                rb2.rotation = -45;
            }
            else if (wandervalue == 7)
            {
                targetpos.x = rb2.position.x - 2;
                targetpos.y = rb2.position.y + 2;
                rb2.rotation = -135;
            }
            else if (wandervalue == 8)
            {
                targetpos.x = rb2.position.x - 2;
                targetpos.y = rb2.position.y - 2;
                rb2.rotation = 45;
            }
            wanderinterval = wanderRate + Time.time;
        }


        movePosition.x = Mathf.MoveTowards(transform.position.x, targetpos.x, pacespeed * Time.deltaTime);
        movePosition.y = Mathf.MoveTowards(transform.position.y, targetpos.y - 2, pacespeed * Time.deltaTime);



        rb2.MovePosition(movePosition);
    }


}
