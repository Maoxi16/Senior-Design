using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceObjectToCamera : MonoBehaviour
{
    void Update()
    {
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);
        }
        else
        {
            Debug.LogWarning("No main camera found. Make sure your camera is tagged as 'MainCamera'.");
        }
    }
}
