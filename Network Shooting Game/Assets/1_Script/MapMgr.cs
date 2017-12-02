using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMgr : MonoBehaviour
{
    public int stageNum;

    public static MapMgr self;

    void Awake()
    {
        if (self == null)
            self = this;
        else if (self != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
