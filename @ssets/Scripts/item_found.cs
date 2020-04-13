using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_found : MonoBehaviour
{
    public GameObject leveldata;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            leveldata.GetComponent<Alert>().itemsfound = leveldata.GetComponent<Alert>().itemsfound + 1;
            Destroy(gameObject);
        }
    }
}
