using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObject : MonoBehaviour {
    public int killPoint = 2;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Damege")
            killPoint--;

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
