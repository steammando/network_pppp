using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundInfo : MonoBehaviour {
    public int x;
    public int y;
    // Use this for initialization
    private void Awake()
    {
        BoxCollider2D tempCol = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.LogError("안전한 땅에 도착했어!");
        }
    }
}
