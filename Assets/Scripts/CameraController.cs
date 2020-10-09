using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSens = 150.0f;
    public Transform playerbody;
    public float yRotation = 0.0f;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        yRotation = mouseX;
        Vector3 target = playerbody.position;
        transform.RotateAround(target, Vector3.up, yRotation);
    }
}
