using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombInfo : MonoBehaviour {

    Vector3 thisBlockPos;


    int damage = 1;

    private void Awake()
    {
        thisBlockPos = transform.position;
        BoxCollider2D tempCol = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("충돌");
            gameObject.SetActive(false);
            col.gameObject.GetComponent<PlayerMove>().Have_Damage(damage);
        }
    }



}
