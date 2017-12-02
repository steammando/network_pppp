using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UnlockStage : MonoBehaviour {
    public Button[] stage;

    void Awake()
    {
        Unlock(MapMgr.self.stageNum);
    }

    void Unlock(int n)
    {
        for (int i = 1; i <= 5; i++)
        {
            if (n >= i)
            {
                stage[i - 1].GetComponent<Button>().interactable = true;
            }
        }
    }
}
