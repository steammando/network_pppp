using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private Camera self;
    private Vector3 moveOrigin;
    private Vector3 prevMouse;

    public bool ballclick;

    public float zoomSpeed = 1f;
    public float zoomMax;
    public float zoomMin;

    public Transform minBound, maxBound;

    // Use this for initialization
    void Start()
    {
        self = GetComponent<Camera>();
        ballclick = false;
    }

    void Update()
    {
        //zoom in
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Zoom(true);
        }
        //zoom out
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Zoom(false);
        }

        if (!ballclick)//if click ball, don't move camera
            if (Input.GetMouseButton(0))
            {
                Moving();
            }
        ClampPosition();

        prevMouse = Input.mousePosition;//save current mouse position
    }

    //mouse move
    void Moving()
    {
        Vector3 move = (Input.mousePosition - prevMouse) * 0.1f; //calculate delta w of mouse position

        self.transform.position = self.transform.position - move;//move camera position
    }

    //limit camera position
    private void ClampPosition()
    {
        float halfHeight = self.orthographicSize;
        float halfWidth = halfHeight * Screen.width / Screen.height;

        float delta;
        if ((delta = transform.position.x - halfWidth - minBound.position.x) < 0)
        {
            transform.position += new Vector3(-delta, 0, 0);
        }
        else if ((delta = transform.position.x + halfWidth - maxBound.position.x) > 0)
        {
            transform.position += new Vector3(-delta, 0, 0);
        }

        if ((delta = transform.position.y - halfHeight - minBound.position.y) < 0)
        {
            transform.position += new Vector3(0, -delta, 0);
        }
        else if ((delta = transform.position.y + halfHeight - maxBound.position.y) > 0)
        {
            transform.position += new Vector3(0, -delta, 0);
        }
    }

    //mouse zoom
    void Zoom(bool inout)
    {
        if (inout)//camera size up
        {
            if (self.orthographicSize < zoomMax)
                self.orthographicSize += zoomSpeed;
        }
        else//camera size down
        {
            if (self.orthographicSize > zoomMin)
                self.orthographicSize -= zoomSpeed;
        }
    }
}