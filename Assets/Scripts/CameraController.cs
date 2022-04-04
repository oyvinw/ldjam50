using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraMoveSpeed = 0.1f;
    public float cameraSmoothness = 1f;

    private float bottomYLimit;
    public float topYLimit = 10;

    private Vector3 camGoal;

    private void Start()
    {
        bottomYLimit = transform.position.y;
        camGoal = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        var camYGoal = Mathf.Clamp(transform.position.y + (Input.GetAxis("Vertical") * cameraMoveSpeed), bottomYLimit, topYLimit);
        camGoal = new Vector3(transform.position.x, camYGoal, transform.position.z);
        */
    }

    private void FixedUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, camGoal, cameraSmoothness);
    }
}
