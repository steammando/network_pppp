using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangnamBall : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Vector3 moveVector;
    private float speed = 5f;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector3(-100f,0,0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.CompareTag("Floor")|| _col.CompareTag("MainFloor"))
        {
            moveVector = rb2d.velocity;
            moveVector.y = moveVector.y * -1;
            rb2d.velocity = moveVector;   
        }
    }
}
