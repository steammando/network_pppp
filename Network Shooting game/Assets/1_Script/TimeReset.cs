using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReset : MonoBehaviour {

    void Awake()
    {
        Time.timeScale = 1f;
    }
}
