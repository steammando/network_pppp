using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPet : MonoBehaviour {

    private Vector3 playerPostition;
    private GameObject bullet;
    private int direction;
	// Use this for initialization
	void Awake () {
        playerPostition = GameManager.instance.player.transform.position;
        if (playerPostition.x < 0)
            direction = -1;
        else direction = 1;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

}
