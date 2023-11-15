using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followPoint;
    [SerializeField] private float followSpeed;

    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;

    private void Start()
    {
        transform.eulerAngles = rotation;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followPoint.position + offset, Time.deltaTime * followSpeed);
    }
}
