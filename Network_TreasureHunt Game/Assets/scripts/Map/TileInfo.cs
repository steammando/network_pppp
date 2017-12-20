using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour {
    public int x;
    public int y;
    public bool active;

    void OnCollisionStay2D(Collision2D col)
    {
    }

    public void destroy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}


