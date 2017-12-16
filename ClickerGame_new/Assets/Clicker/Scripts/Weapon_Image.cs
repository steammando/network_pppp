using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Clicker
{
    public class Weapon_Image : MonoBehaviour
    {


        private SpriteRenderer sr;
        public Sprite goldSpoon;
        public Sprite pixaxe;
        public Sprite sward;

        public Text textMessage; 
        public static Weapon_Image Instance = null;
        // Use this for initialization
        void Awake()
        {
            Instance = this;
            sr = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        public void changeImage(int damage)
        {
            if (damage > 0 && damage < 20)
            {
                sr.sprite = goldSpoon;
                textMessage.text = "Gold Spoon";
            }
            else if (damage < 30)
            {
                sr.sprite = sward;
                textMessage.text = "Sward";
            }
            else
            {
                sr.sprite = pixaxe;
                textMessage.text = "Super Pixaxe";
            }
        }
    }
}
