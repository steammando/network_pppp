using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineInfo : MonoBehaviour {

    //Adjust the price and recovery amount.
    int Price = 5;
    float Value = 1f;

    private void Awake()
    {
        //Create and save variables for conflict determination.
        BoxCollider2D tempCol = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("상점 도착");
            col.gameObject.GetComponent<PlayerMove>().BuyPortion(Price, Value);
            //Call BuyPortion function in PlayerMove script
        }
    }
}
