using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfBlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
       
    }
    public void goDown()
    {
        {
            //gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<EdgeCollider2D>().isTrigger = true;
        }
    }
    void OnCollisionEnter2D(Collision2D _col)
    {
        if (_col.gameObject.tag == "Head")
        {
           // gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<EdgeCollider2D>().isTrigger = true;
        }
    }
    void OnCollisionExit2D(Collision2D _col)
    {
        if (_col.gameObject.tag == "Foot")
        {
            // gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            Debug.Log("Attach");
            gameObject.GetComponent<EdgeCollider2D>().isTrigger = false;
        }
    }
    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.gameObject.tag == "Head")
        {
            //gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<EdgeCollider2D>().isTrigger = true;
        }
    }
    void OnTriggerExit2D(Collider2D _col)
    {
        if (_col.gameObject.tag == "Foot")
        {
            //gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            Debug.Log("Attach2");
            gameObject.GetComponent<EdgeCollider2D>().isTrigger = false;
        }
    }
}
