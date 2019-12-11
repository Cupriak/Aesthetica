using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform targetTransform;

    [SerializeField] bool isStatic;

    void FocusOn(Transform target)
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if(!isStatic)
        {
            FocusOn(targetTransform);
        }
    }
}
