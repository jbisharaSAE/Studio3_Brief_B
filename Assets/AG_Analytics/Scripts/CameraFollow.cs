using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    void LateUpdate()
    {
        if (target)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}
