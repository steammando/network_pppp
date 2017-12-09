﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : MonoBehaviour {
    public GameObject mini;
    public float updownPower;

    private GameObject up;
    private GameObject mid;
    private GameObject down;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            up = Instantiate(mini, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
            mid = Instantiate(mini, transform.position, Quaternion.identity);
            down = Instantiate(mini, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);

            up.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
            mid.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
            down.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;

            up.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, updownPower));
            down.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -updownPower));

            Destroy(this.gameObject);
        }
	}
}