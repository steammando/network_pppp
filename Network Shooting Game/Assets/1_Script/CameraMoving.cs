using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour {
    private Camera self;
    private Vector3 moveOrigin;
    private Vector3 prevMouse;

    public bool ballclick;
    
    public float zoomSpeed = 1f;
    public float zoomMax;
    public float zoomMin;

	// Use this for initialization
	void Start () {
        self = GetComponent<Camera>();
        ballclick = false;
	}

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)//zoom in
        {
            Zoom(true);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)//zoom out
        {
            Zoom(false);
        }

        if (!ballclick)
            if (Input.GetMouseButton(0))
            {
                Moving();
            }

        prevMouse = Input.mousePosition;
    }

    void Moving()
    {
        Vector3 move = (Input.mousePosition - prevMouse) * 0.1f;

        move.x = -move.x;
        move.y = -move.y;

        self.transform.position = self.transform.position + move;
    }

    void Zoom(bool inout)
    {
        if (inout)
        {
            if(self.orthographicSize < zoomMax)
            self.orthographicSize += zoomSpeed;
        }
        else
        {
            if (self.orthographicSize > zoomMin)
                self.orthographicSize -= zoomSpeed;
        }
    }
}
