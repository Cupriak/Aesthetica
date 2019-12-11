using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;

    [HideInInspector] public bool isStatic;

    private void Awake()
    {
        transform.position = playerTransform.position;
    }

    public void FocusOn(Transform target)
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void FixedUpdate()
    {
        if(!isStatic)
        {
            if(playerTransform != null)
            {
                FocusOn(playerTransform);
            }
            else
            {
                isStatic = true;
            }
        }
    }
}
