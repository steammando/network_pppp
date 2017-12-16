using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatSelect : MonoBehaviour
{
    public bool check = false;

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Ball")
        {
            Destroy(c.gameObject);
            check = true;
        }
    }
}