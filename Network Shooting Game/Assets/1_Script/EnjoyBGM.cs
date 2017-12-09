using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnjoyBGM : MonoBehaviour {
    static EnjoyBGM my = null;

	void Awake()
    {
        if (my == null)
        {
            my = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
}
