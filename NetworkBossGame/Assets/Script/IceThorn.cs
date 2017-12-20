using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceThorn : MonoBehaviour {
    //when it collision with floor, (Destroy)
    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("MainFloor") || _col.CompareTag("Floor")||_col.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
