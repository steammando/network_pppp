using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //SceneManager를 사용하기 위해서 추가한다.


public class UIgameover : MonoBehaviour {

    // Use this for initialization
    public void RetryButton()
    {
        SceneManager.LoadScene("Level1"); //만들어둔 씬 불러오기, 레벨마다 다르면 전 레벨 불러오기로
        Time.timeScale = 1f; //시간 크기를 다시 원래대로 되돌려줌
    }
    public void B2Menu()
    {
        Time.timeScale = 0;
        GameObject.Find("Canvas").transform.Find("GameStartPanel").gameObject.SetActive(true);
    }
}
