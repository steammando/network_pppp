using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineInfo : MonoBehaviour {

    int Price = 5;
    float Value = 1f;



    private void Awake()
    {
        BoxCollider2D tempCol = gameObject.GetComponent<BoxCollider2D>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.LogError("상점 도착");
            col.gameObject.GetComponent<PlayerMove>().BuyPortion(Price, Value);
            /*
            gameObject.SetActive(false);
            Destroy(gameObject);
            */
        }
    }
}
