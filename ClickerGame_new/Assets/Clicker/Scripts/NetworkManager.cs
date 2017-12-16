using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Clicker {
    public class NetworkManager : MonoBehaviour {

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.Space))
                WallManager.Instance.makeNewBlock();
    
    }
    }
}