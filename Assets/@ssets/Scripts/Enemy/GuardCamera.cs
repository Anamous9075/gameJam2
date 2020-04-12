using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuardCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField, Tooltip("How fast the camera rotating")] private float rotationSpeed;
    [SerializeField] private Vector3 targetRotation;

    private Tween rotatingTween;
    private GameObject detectedPlayer;

    private void Start()
    {
        Rotate();
    }

    private void Rotate()
    {
        if(rotatingTween!=null)
        {
            rotatingTween.Kill();
            rotatingTween = null;
        }

        rotatingTween = cameraTransform.DORotate(targetRotation, rotationSpeed)
            .SetLoops(-1)
            .SetEase(Ease.Linear)
            .OnUpdate(() => 
            { 

            })
            .OnStepComplete(()=> 
            { 

            });
    }

    private void Scan()
    {
        if (detectedPlayer != null)
        {
            Attack();
        }
        else
        {
            Rotate();
        }
    }

    private void Attack()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            detectedPlayer = collision.gameObject;
            rotatingTween.Kill();
            Scan();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            detectedPlayer = null;
        }
    }
}
