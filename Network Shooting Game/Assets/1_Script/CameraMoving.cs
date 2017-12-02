using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour {
    private Camera self;
    private Vector3 moveOrigin;

    public float moveSpeed = 2f;
    public float zoomSpeed = 1f;
    public float zoomMax;
    public float zoomMin;

	// Use this for initialization
	void Start () {
        self = GetComponent<Camera>();
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

        if (Input.GetMouseButtonDown(0))
        {
            moveOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;


        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - moveOrigin);
        Vector3 move = new Vector3(-pos.x * moveSpeed, -pos.y * moveSpeed, 0);

        transform.Translate(move, Space.World);
    }

    void Moving()
    {

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
