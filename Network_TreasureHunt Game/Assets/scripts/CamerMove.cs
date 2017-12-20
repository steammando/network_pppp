using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMove : MonoBehaviour {

    

    GameObject player;
    private Vector3 tempVector;
    // Use this for initialization

    private void Awake()
    {
        Time.timeScale = 0;
        GameObject.Find("Canvas").transform.Find("GameStartPanel").gameObject.SetActive(true);
    }

    void Start ()
    {
        GameObject.Find("BoardManager").transform.Find("Player").gameObject.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        tempVector = player.transform.position;
        tempVector.z = -10;
        gameObject.transform.position = tempVector;
	}
}
