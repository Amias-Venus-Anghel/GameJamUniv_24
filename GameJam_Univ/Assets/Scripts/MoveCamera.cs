using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCamera : MonoBehaviour
{
    private float speed;
    // we use camera size to zoom in and out
    // bigger size = zoom out ; smaller size = zoom in
    private float zoomSensibility = 100f;
    private Camera cam;
    private float maxSize = 400, minSize = 30;
    private Vector3 initSizeV;
    private float initSize = 100;
    private float xLimitMin = 50, xLimitMax = 100, yLimitMin = 400, yLimitMax = 450;
    private float minSpeed = 100, maxSpeed = 200;
    Vector3 initPos = new Vector3(51, 390, -10);

    void Start() {
        cam = GetComponent<Camera>();
        initSize = cam.orthographicSize;
        initSizeV = /*new Vector3(1.7f, 2, 1) */ transform.localScale / initSize;
        speed = minSpeed;
        initPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(transform.position.x, xLimitMin, xLimitMax),
            Mathf.Clamp(transform.position.y, yLimitMin, yLimitMax),
            Mathf.Clamp(transform.position.z, transform.position.z, transform.position.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, zoomSensibility * Time.fixedDeltaTime);
        transform.position = smoothPosition;

        // zoom in
        if ((Input.GetKey(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") > 0) && cam.orthographicSize >= minSize)
        {   
            if (Input.GetKey(KeyCode.E))
                cam.orthographicSize -= zoomSensibility * Time.deltaTime;
            else
                cam.orthographicSize -= zoomSensibility * 4 * Time.deltaTime;

            if (cam.orthographicSize < (minSize+maxSize)/2)
                speed = minSpeed;

            float scalingTime = Time.time * zoomSensibility;
            transform.localScale = new Vector3(
                Mathf.PingPong(scalingTime, 0.0001f) + cam.orthographicSize * initSizeV.x,
                Mathf.PingPong(scalingTime, 0.0001f) + cam.orthographicSize * initSizeV.y, 1
            );

        }
        // zoom out
        if ((Input.GetKey(KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") < 0) && cam.orthographicSize <= maxSize)
        {
            if (Input.GetKey(KeyCode.Q))
                cam.orthographicSize += zoomSensibility * Time.deltaTime;
            else
                cam.orthographicSize += zoomSensibility * 4 * Time.deltaTime;
            if (cam.orthographicSize > (minSize + maxSize) / 2)
                speed = maxSpeed;

            float scalingTime = Time.time * zoomSensibility;
            transform.localScale = new Vector3(
                Mathf.PingPong(scalingTime, 0.0001f) + cam.orthographicSize * initSizeV.x,
                Mathf.PingPong(scalingTime, 0.0001f) + cam.orthographicSize * initSizeV.y, 1
            );
        }

    }

    public void IncreaseMaxSize(Vector3 cardPos)
    {
        if (cardPos.x < xLimitMin)
            xLimitMin -= 50;

        if (cardPos.x > xLimitMax)
            xLimitMax += 50;

        if (cardPos.y > yLimitMax)
            yLimitMax += 50;

        if (cardPos.y < yLimitMin)
            yLimitMin -= 50;
    }

    public void ResetCamera()
    {
        xLimitMin = 50;
        xLimitMax = 100;
        yLimitMin = 400;
        yLimitMax = 450;
        GetComponent<Camera>().orthographicSize = initSize;
        this.GetComponent<Transform>().transform.position = initPos;
    }
}
