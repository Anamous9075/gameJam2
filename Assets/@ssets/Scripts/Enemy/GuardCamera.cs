using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class GuardCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField, Tooltip("How fast the camera rotating")] private float rotationDuration;
    [SerializeField] private Vector3 targetRotation;
    [SerializeField] private UnityEvent onTargetDetected;
    [SerializeField] private UnityEvent onTargetUndetected;

    private Tween rotatingTween;

    private Coroutine chasingCoroutine;

    private GameObject detectedPlayer;

    private void Start()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (rotatingTween != null)
        {
            rotatingTween.Kill();
            rotatingTween = null;
        }

        rotatingTween = cameraTransform.DORotate(targetRotation, rotationDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {

            })
            .OnStepComplete(() =>
            {

            });
    }

    private void Scan()
    {
        if (detectedPlayer != null)
        {
            Chase();
            if(onTargetDetected!=null)
            {
                onTargetDetected.Invoke();
            }
        }
        else
        {
            Rotate();
            if(onTargetUndetected!=null)
            {
                onTargetUndetected.Invoke();
            }
        }
    }

    private void Chase()
    {
        if (chasingCoroutine != null)
        {
            StopCoroutine(chasingCoroutine);
        }

        chasingCoroutine = StartCoroutine(ChaseIE());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            detectedPlayer = collision.gameObject;
            rotatingTween.Kill();
            Scan();
        }
        Debug.Log("Collide Enter with something");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            detectedPlayer = null;
        }
        Debug.Log("Collide Exit with something");
    }

    private IEnumerator ChaseIE()
    {
        while (detectedPlayer != null)
        {
            Vector2 direction = (Vector2)detectedPlayer.transform.position - (Vector2)cameraTransform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            var temp = Quaternion.Euler(0f, 0f, angle);

            yield return cameraTransform.DORotate(temp.eulerAngles, .5f)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {

            })
            .OnStepComplete(() =>
            {

            }).WaitForCompletion();

            yield return null;
        }
        Scan();
    }
}
