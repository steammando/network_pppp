using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIgameStart : MonoBehaviour
{
    // Use this for initialization
    public void StartButton()
    {
        Time.timeScale = 1f;
        GameObject.Find("Canvas").transform.Find("GameStartPanel").gameObject.SetActive(false);
        GameObject.Find("BoardManager").transform.Find("GameManager").gameObject.SetActive(true);
        GameObject.Find("BoardManager").transform.Find("Player").gameObject.SetActive(true);
    }
    public void ExitButton()
    {

    }
    public void Networkbutton()
    {

    }
}
