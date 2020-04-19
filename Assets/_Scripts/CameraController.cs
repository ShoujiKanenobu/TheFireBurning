using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public float smoothing;
    public GameObject target;

    private void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, target.transform.position + offset, smoothing);
    }
}
