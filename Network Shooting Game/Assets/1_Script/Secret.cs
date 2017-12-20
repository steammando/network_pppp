using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret : MonoBehaviour {
    public GameObject[] hats;
    public GameObject[] rockets;

    public int secretNum;

    //secret num setting
	void Start () {
        secretNum = Random.Range(0, hats.Length);
	}
	
	void Update () {
        if(hats[secretNum].GetComponent<HatSelect>().check == true)//check hat is selected
        {
            LaunchRocket();
        }
	}

    //launch rocket booooooooooooong!
    void LaunchRocket()
    {
        foreach(GameObject go in rockets)
        {
            go.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
