using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{

    public bool isLeft;

    enum state
    {
        attack, idle
    };
    private Vector3 bossPosition;
    private Vector3 objectPosition;
    private state currentState;

    // Use this for initialization
    void Start()
    {
        currentState = state.idle;
    }

    // Update is called once per frame
    void Update()
    {
        bossPosition = GameManager.instance.boss.transform.position;
        if (currentState == state.idle)
        {
            objectPosition = bossPosition;
            if (isLeft)
            {    
                objectPosition.x -= 3f;
                gameObject.transform.position = objectPosition;
            }
            else
            {
                Debug.Log("why?");
                objectPosition.y = -3f;
                objectPosition.x += 3f;
                gameObject.transform.position = objectPosition;
            }
        }
    }
}
