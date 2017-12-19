using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

    public LineRenderer front;
    public LineRenderer back;
    public float maxStretch = 3.0f;

    public GameObject cameraObj;//use camera move

    private bool clikedOn;
    private bool clikOnce;
    private SpringJoint2D spring;
    private Vector2 prevVelocity;
    private Ray leftCatapultToProjectile;
    private float circleRadius;
    private Transform catapult;
    private Ray rayToMouse;

    //seting first
    void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        spring.connectedBody = GameObject.Find("Catapult_1").GetComponent<Rigidbody2D>();
        catapult = spring.connectedBody.transform;

        cameraObj = GameObject.Find("Main Camera");
    }
    //seting first
    void Start() {
        front = GameObject.Find("Catapult_1").GetComponent<LineRenderer>();
        back = GameObject.Find("Catapult_2").GetComponent<LineRenderer>();
        LineRendererSetup();

        front.enabled = true;
        back.enabled = true;

        rayToMouse = new Ray(catapult.position, Vector3.zero);
        leftCatapultToProjectile = new Ray(front.transform.position, Vector3.zero);
        circleRadius = GetComponent<CircleCollider2D>().radius / 2;

        clikOnce = false;
    }

    void Update() {
        //if click ball , dragging
        if (!clikOnce && clikedOn)
            Dragging();

        //ball is launched
        if (spring != null)
        {
            if (!GetComponent<Rigidbody2D>().isKinematic && prevVelocity.sqrMagnitude > GetComponent<Rigidbody2D>().velocity.sqrMagnitude)
            {
                Destroy(spring);
                clikOnce = true;
                GetComponent<Rigidbody2D>().velocity = prevVelocity;
                GetComponent<Rigidbody2D>().mass = 2f;
            }
        }
        else
        {
            if (front != null)
            {
                front.enabled = false;
                back.enabled = false;
            }
            front = null;
            back = null;
        }

        prevVelocity = GetComponent<Rigidbody2D>().velocity;

        //band update
        if (front != null)
            LineRendererUpdate();

    }

    void OnMouseUp()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        clikedOn = false;

        cameraObj.GetComponent<CameraMoving>().ballclick = false;
    }

    void OnMouseDown()
    {
        clikedOn = true;

        cameraObj.GetComponent<CameraMoving>().ballclick = true;
    }

    //drag ball
    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint - catapult.position;

        if (catapultToMouse.sqrMagnitude > maxStretch * maxStretch)
        {
            rayToMouse.direction = catapultToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
        }

        mouseWorldPoint.z = 0f;
        transform.position = mouseWorldPoint;
    }
    //band rendering set up
    void LineRendererSetup()
    {
        front.SetPosition(0, front.transform.position);
        back.SetPosition(0, back.transform.position);

        front.sortingLayerName = "Foreground";
        back.sortingLayerName = "Foreground";

        front.sortingOrder = 3;
        back.sortingOrder = 1;
    }
    //band rendering update
    void LineRendererUpdate()
    {
        Vector2 catapultToProjectile = transform.position - front.transform.position;
        leftCatapultToProjectile.direction = catapultToProjectile;

        Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + circleRadius);

        front.SetPosition(1, holdPoint);
        back.SetPosition(1, holdPoint);
    }
}
