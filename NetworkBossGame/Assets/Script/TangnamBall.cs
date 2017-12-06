using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangnamBall : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Vector3 moveVector;
    private int TTL;
    private float speed = 5f;
	// Use this for initialization
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
        TTL = 3;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void AddForce(int direction)
    {
        if(direction==1)
            rb2d.AddForce(new Vector3(Random.Range(100,200), 400, 0));
        else
            rb2d.AddForce(new Vector3(Random.Range(-200, -100), 400, 0));
    }
    void OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.CompareTag("Floor")|| _col.CompareTag("MainFloor"))
        {
            TTL--;
            if (TTL == 0)
                Destroy(gameObject);
            moveVector = rb2d.velocity;
            moveVector.y = moveVector.y * -1;
            rb2d.velocity = moveVector;   
        }
    }
}
