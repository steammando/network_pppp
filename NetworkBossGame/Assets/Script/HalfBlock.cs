using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfBlock : MonoBehaviour {
    public void goDown()
    {
        {
            //gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<EdgeCollider2D>().isTrigger = true;
        }
    }
    //if collide with head -->trigger
    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.gameObject.tag == "Head")
        {
            gameObject.GetComponent<EdgeCollider2D>().isTrigger = true;
        }
    }
    //if collide with foot --> collision
    void OnTriggerExit2D(Collider2D _col)
    {
        if (_col.gameObject.tag == "Foot")
        {
            gameObject.GetComponent<EdgeCollider2D>().isTrigger = false;
        }
    }
    
}
