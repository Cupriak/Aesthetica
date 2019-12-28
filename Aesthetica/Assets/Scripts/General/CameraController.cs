using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controlls camera
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Transform of player that will be followed
    /// </summary>
    [SerializeField] private Transform playerTransform;
    /// <summary>
    /// Smoothing speed. Bigger value means slower camera movement
    /// </summary>
    [Range(0, 1)][SerializeField] private float smoothSpeed;
    /// <summary>
    /// Camera offset.
    /// </summary>
    [SerializeField] private Vector3 offset;

    /// <summary>
    /// Main camera reference
    /// </summary>
    private Camera mainCamera;
    /// <summary>
    /// Zoom of the camera
    /// </summary>
    [Range(1, 10)][SerializeField] private float cameraZoom;
    /// <summary>
    /// default value of camera zoom
    /// </summary>
    private float defaultZoom = 2.424129f;

    /// <summary>
    /// Flag that allowes camera zoom
    /// </summary>
    [SerializeField] private bool allowCameraZoom;

    /// <summary>
    /// Flag that allows camera to follow the player or make it static
    /// </summary>
    [HideInInspector] public bool isStatic;

    /// <summary>
    /// Call on object creation. Set initial values
    /// </summary>
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        mainCamera.orthographicSize = defaultZoom;
        transform.position = playerTransform.position;
    }

    /// <summary>
    /// Linearly interpolates position of camera to target
    /// </summary>
    /// <param name="target"></param>
    public void FocusOn(Transform target)
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    /// <summary>
    /// Called fixed times per second.
    /// Updates camera zoom and pozition
    /// </summary>
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
