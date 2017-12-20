using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {
    //when collide with wall...destroy
    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
