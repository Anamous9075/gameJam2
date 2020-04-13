using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapWire : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private UnityEvent onTargetDetected;

    private GameObject targetObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(targetTag))
        {
            targetObj = collision.gameObject;
            if(onTargetDetected!=null)
            {
                onTargetDetected.Invoke();
            }
        }
    }
}
