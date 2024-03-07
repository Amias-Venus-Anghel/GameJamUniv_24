using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private float edge = 1f;
    private float speed = 300;
    // we use camera size to zoom in and out
    // bigger size = zoom out ; smaller size = zoom in
    private float zoomSensibility = 100f;
    private Camera cam; 

    void Start() {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 dir = new Vector3(0, 0, 0);
        // if (Input.mousePosition.x > Screen.width - edge || Input.GetKey(KeyCode.D))
        //     dir = Vector3.right;
        // if (Input.mousePosition.x < edge || Input.GetKey(KeyCode.A))
        //     dir = Vector3.left;
        // if (Input.mousePosition.y < edge || Input.GetKey(KeyCode.S))
        //     dir = Vector3.down;
        // if (Input.mousePosition.y > Screen.height - edge || Input.GetKey(KeyCode.W))
        //     dir = Vector3.up;
        // transform.position = transform.position + dir * speed * Time.deltaTime;

        Vector3 dir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.D))
            dir = Vector3.right;
        if (Input.GetKey(KeyCode.A))
            dir = Vector3.left;
        if (Input.GetKey(KeyCode.S))
            dir = Vector3.down;
        if (Input.GetKey(KeyCode.W))
            dir = Vector3.up;
        transform.position = transform.position + dir * speed * Time.deltaTime;

        // zoom in
        if (Input.GetKey(KeyCode.E)) {
            cam.orthographicSize -= zoomSensibility * Time.deltaTime;
        }
        // zoom out
        if (Input.GetKey(KeyCode.Q)) {
            cam.orthographicSize += zoomSensibility * Time.deltaTime;
        }
    }
}
