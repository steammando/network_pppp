using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMove : MonoBehaviour {

    //Create a variable to store the player's position and an empty variable to calculate the position
    GameObject player;
    private Vector3 tempVector;


    private void Awake()
    {
        //when game start, Stop the time, and call up the start menu.
        Time.timeScale = 0;
        GameObject.Find("Canvas").transform.Find("GameStartPanel").gameObject.SetActive(true);
        //The selection sheet exists in UIgamestart, and the result of the selection is processed by the script.
    }

    void Start ()
    {
        GameObject.Find("BoardManager").transform.Find("Player").gameObject.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
        //Then, the camera stores the player's position to follow the player.
    }

    // Update is called once per frame
    void Update () {
        tempVector = player.transform.position;
        tempVector.z = -10;
        gameObject.transform.position = tempVector;
        //The camera's position keeps track of the player's position in real time, and since it is a 2D game, it adjusts the z-axis to -10.
    }
}
