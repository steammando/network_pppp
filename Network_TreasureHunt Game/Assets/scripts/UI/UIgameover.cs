using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Add it to use the SceneManager.


public class UIgameover : MonoBehaviour {

    public void RetryButton() //when click the retry button
    {
        SceneManager.LoadScene("Level1"); //scene you created is loaded.
        Time.timeScale = 1f; //Allow time to flow again.
    }
}
