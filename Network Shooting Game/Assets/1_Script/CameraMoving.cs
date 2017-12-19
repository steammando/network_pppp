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

        prevMouse = Input.mousePosition;//save current mouse position
    }

    //mouse move
    void Moving()
    {
        Vector3 move = (Input.mousePosition - prevMouse) * 0.1f; //calculate delta w of mouse position

        self.transform.position = self.transform.position - move;//move camera position
    }

    //mouse zoom
    void Zoom(bool inout)
    {
        if (inout)//camera size up
        {
            if(self.orthographicSize < zoomMax)
            self.orthographicSize += zoomSpeed;
        }
        else//camera size down
        {
            if (self.orthographicSize > zoomMin)
                self.orthographicSize -= zoomSpeed;
        }
    }
}
