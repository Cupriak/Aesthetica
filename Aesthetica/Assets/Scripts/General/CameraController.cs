using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [Range(0, 1)][SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;

    private Camera mainCamera;
    [Range(1, 10)][SerializeField] private float cameraZoom;
    private float defaultZoom = 2.424129f;

    [SerializeField] private bool allowCameraZoom;

    [HideInInspector] public bool isStatic;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        mainCamera.orthographicSize = defaultZoom;
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
        if(allowCameraZoom)
        {
            mainCamera.orthographicSize = cameraZoom;
        }
        else
        {
            mainCamera.orthographicSize = defaultZoom;
        }

        if (!isStatic)
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
