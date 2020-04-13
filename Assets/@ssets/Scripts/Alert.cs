using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public bool alerted = false;
    public int itemsfound = 0;
    public void alert()
    {
        alerted = true;
        Debug.Log("alerted!");
    }
}
