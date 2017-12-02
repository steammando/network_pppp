using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAni : MonoBehaviour {
    public Sprite spr;

    void Update()
    {
        if (this.GetComponent<SpriteRenderer>().sprite == spr)
            Destroy(this.gameObject);
    }
}
