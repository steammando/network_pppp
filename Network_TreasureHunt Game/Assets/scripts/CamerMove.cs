using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMove : MonoBehaviour {
    GameObject player;
    private Vector3 tempVector;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("AAA");
        tempVector= player.transform.position;
        tempVector.z = -10;
        gameObject.transform.position = tempVector;
	}
}
