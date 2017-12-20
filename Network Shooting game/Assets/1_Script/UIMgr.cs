using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIMgr : MonoBehaviour {
    
    public void OnClickMenuButton(GameObject menu)//menu click
    {
        menu.SetActive(!menu.activeSelf);

        if (menu.activeSelf)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void OnClickGoMain()//go main menu
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickGoMap()//go stage select menu
    {
        SceneManager.LoadScene("Map");
    }

    public void OnClickExit()//game exit
    {
        SocketCon.instance.endSocketCon();
        Application.Quit();
    }

    public void OnClickGoStage(int n)//go to stage n
    {
        SceneManager.LoadScene("Stage" + n);
    }

    public void OnClickRe(int n)//re start stage n
    {
        //restart
        SceneManager.LoadScene("Stage" + n);
    }

    public void OnClickNext(int stageNum)//go to next stage
    {
        SceneManager.LoadScene("Stage" + (stageNum + 1));
    }
}
