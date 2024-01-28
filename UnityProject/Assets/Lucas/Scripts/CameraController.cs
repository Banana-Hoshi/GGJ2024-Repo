using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetTrans;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float horizontalOffset = 0f;
    [SerializeField] private float verticalOffset = -5f;


    private void Update()
    {
        Quaternion yRot = Quaternion.Euler(0f, targetTrans.eulerAngles.y, 0f);
        transform.position = targetTrans.position + yRot * cameraOffset;
        transform.rotation = Quaternion.Euler(verticalOffset, targetTrans.eulerAngles.y + horizontalOffset, 0f);
    }
}
