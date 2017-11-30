using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

    public LineRenderer front;
    public LineRenderer back;
    public float maxStretch = 3.0f;

    private bool clikedOn;
    private SpringJoint2D spring;
    private Vector2 prevVelocity;
    private Ray leftCatapultToProjectile;
    private float circleRadius;
    private Transform catapult;
    private Ray rayToMouse;

    void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        catapult = spring.connectedBody.transform;
    }

    void Start() {
        LineRendererSetup();
        rayToMouse = new Ray(catapult.position, Vector3.zero);
        leftCatapultToProjectile = new Ray(front.transform.position, Vector3.zero);
        circleRadius = GetComponent<CircleCollider2D>().radius / 2;
    }

    void Update() {

        if (clikedOn)
            Dragging();

        if (spring != null)
        {
            if (!GetComponent<Rigidbody2D>().isKinematic && prevVelocity.sqrMagnitude > GetComponent<Rigidbody2D>().velocity.sqrMagnitude)
            {
                Destroy(spring);

                GetComponent<Rigidbody2D>().velocity = prevVelocity;
            }
        }
        else
        {
            front.enabled = false;
            back.enabled = false;
        }

        prevVelocity = GetComponent<Rigidbody2D>().velocity;

        LineRendererUpdate();

    }

    void OnMouseUp()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        clikedOn = false;
    }

    void OnMouseDown()
    {
        clikedOn = true;
    }

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

    void LineRendererSetup()
    {
        front.SetPosition(0, front.transform.position);
        back.SetPosition(0, back.transform.position);

        front.sortingLayerName = "Foreground";
        back.sortingLayerName = "Foreground";

        front.sortingOrder = 3;
        back.sortingOrder = 1;
    }

    void LineRendererUpdate()
    {
        Vector2 catapultToProjectile = transform.position - front.transform.position;
        leftCatapultToProjectile.direction = catapultToProjectile;

        Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + circleRadius);

        front.SetPosition(1, holdPoint);
        back.SetPosition(1, holdPoint);
    }
}
