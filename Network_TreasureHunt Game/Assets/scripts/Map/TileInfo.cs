using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour {
    public int x;
    public int y;
    public bool active;
    bool dig;

    Vector3 thisBlockPos;

    private void Awake()
    {
        thisBlockPos = transform.position;
        BoxCollider2D tempCol = gameObject.GetComponent<BoxCollider2D>();
    }

    
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("tile[" + y + "," + x + "]과 충돌");

        dig = col.gameObject.GetComponent<PlayerMove>().dig_mode;

        active = false;
    }

    public void check_active()
    {
        if (active == false && dig == true)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}


