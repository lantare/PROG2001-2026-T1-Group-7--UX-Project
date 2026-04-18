using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow the goal")]
    public Transform target;  

    [Header("Camera parameters")]
    public float distance = 4f;     
    public float height = 2f;       
    public float smoothSpeed = 8f; 

    void LateUpdate()
    {
        if (target == null) return;

       
        Vector3 targetPos = target.position - target.forward * distance;
        targetPos.y = target.position.y + height;

        
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        transform.LookAt(target);
    }
}
