using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIgameStart : MonoBehaviour
{
    public void StartButton()
    {
        //Start the game without network settings. The number of bombs is fixed at 17.
        Time.timeScale = 1f;
        GameObject.Find("Canvas").transform.Find("GameStartPanel").gameObject.SetActive(false);
        GameObject.Find("BoardManager").transform.Find("GameManager").gameObject.SetActive(true);
        GameObject.Find("BoardManager").transform.Find("Player").gameObject.SetActive(true);
    }
    public void ExitButton()
    {
        //Quit Application, with close socket connect
        NetworkConsole.instance.endSocketCon();
        Application.Quit();
    }
    public void Networkbutton()
    {
        //Start the game with network connection, Create Bomb through Vote
        NetworkConsole.instance.startVote();
        Time.timeScale = 1f;
        GameObject.Find("Canvas").transform.Find("GameStartPanel").gameObject.SetActive(false);
        GameObject.Find("BoardManager").transform.Find("NetworkGameManager").gameObject.SetActive(true);
        GameObject.Find("BoardManager").transform.Find("Player").gameObject.SetActive(true);
    }
}
