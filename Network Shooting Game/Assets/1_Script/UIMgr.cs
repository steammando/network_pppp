using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIMgr : MonoBehaviour {
    
    public void OnClickMenuButton(GameObject menu)
    {
        menu.SetActive(!menu.active);

        if (menu.active)
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
}
