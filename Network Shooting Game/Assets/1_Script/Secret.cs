using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret : MonoBehaviour {
    public GameObject[] hats;
    public GameObject[] rockets;

    public int secretNum;

	// Use this for initialization
	void Start () {
        secretNum = Random.Range(0, hats.Length);
	}
	
	// Update is called once per frame
	void Update () {
        if(hats[secretNum].GetComponent<HatSelect>().check == true)
        {
            LaunchRocket();
        }
	}

    void LaunchRocket()
    {
        foreach(GameObject go in rockets)
        {
            go.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
