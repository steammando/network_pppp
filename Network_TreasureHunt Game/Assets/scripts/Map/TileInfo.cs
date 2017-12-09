using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour {
    public int x;
    public int y;
    public bool active;

    Vector3 thisBlockPos;

    private void Awake()
    {
        thisBlockPos = transform.position;
        BoxCollider2D tempCol = gameObject.GetComponent<BoxCollider2D>();
    }

    
    void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log("tile[" + y + "," + x + "]과 충돌");
    }

    public void destroy()
    {
       
        gameObject.SetActive(false);
        Destroy(gameObject);
        
    }
}


