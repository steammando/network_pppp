using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_collision : MonoBehaviour {

    public bool active;
    private TileInfo currentTile = null;
    
    private void Awake()
    {
        BoxCollider2D tempCol = gameObject.GetComponent<BoxCollider2D>();
    } 


    void update()
    {
        if(active == false)
        {
            gameObject.SetActive(false);
        }
        else if(active == true){
            gameObject.SetActive(true);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        int j = col.gameObject.GetComponent<TileInfo>().x;
        int i = col.gameObject.GetComponent<TileInfo>().y;
        currentTile = col.gameObject.GetComponent<TileInfo>();
    }

    void Dest_tile()
    {
        if (currentTile != null)
            GameObject.Destroy(currentTile.gameObject); //충돌 체크된 타일을 삭제시킨다.
    }

}
