using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

    // Use this for initialization
    private SpriteRenderer sr;
    private Color color;
    private int damage=10;
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        //color.a = 0f;
        color = sr.color;
        color.a = 0f;
        sr.color = color;

    }
	void OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.CompareTag("Player")&&sr.color.a!=0)
        {
            Debug.Log("Damaged!");
            GameManager.instance.player.Damaged(damage);
        }
    }
	// Update is called once per frame
	void Update () {
        
    }
}
