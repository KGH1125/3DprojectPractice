using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

}
