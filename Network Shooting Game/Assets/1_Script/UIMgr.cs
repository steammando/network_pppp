using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIMgr : MonoBehaviour {
    
    public void OnClickMenuButton(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);

        if (menu.activeSelf)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void OnClickGoMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickGoMap()
    {
        SceneManager.LoadScene("Map");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickGoStage(int n)
    {
        SceneManager.LoadScene("Stage" + n);
    }

    public void OnClickRe()
    {
        //restart
        //SceneManager.LoadScene();
    }
}
