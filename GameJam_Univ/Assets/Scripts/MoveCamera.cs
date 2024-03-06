using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private float edge = 1f;
    private float speed = 300;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(0, 0, 0);
        if (Input.mousePosition.x > Screen.width - edge)
            dir = Vector3.right;
        if (Input.mousePosition.x < edge)
            dir = Vector3.left;
        if (Input.mousePosition.y < edge)
            dir = Vector3.down;
        if (Input.mousePosition.y > Screen.height - edge)
            dir = Vector3.up;
        transform.position = transform.position + dir * speed * Time.deltaTime;
    }
}
