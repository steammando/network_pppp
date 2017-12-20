using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObject : MonoBehaviour {
    public int killPoint = 2;

    void OnCollisionEnter2D(Collision2D coll)
    {
        //object get damege
        if (coll.gameObject.tag == "Damege")
            killPoint--;
        if (coll.gameObject.tag == "Ball")
            Kill();

        //object is dead
        if(killPoint <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        Destroy(this.gameObject);
    }
}
