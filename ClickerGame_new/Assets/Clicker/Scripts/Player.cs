using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Clicker
{
    public class Player : MonoBehaviour
    {
        public Text Damage;
        public GameObject particle;
        public AudioClip explosion;
        private Weapon[] weapons;
        private Animator anim;
        private AudioSource myaudio;
       
        private int currentWeaponIndex = -1;

        public int PlayerDamage { private set; get; }

        public static Player Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
            anim = GetComponent<Animator>();
            myaudio = GetComponent<AudioSource>();
            weapons = GetComponentsInChildren<Weapon>();
            foreach (var weapon in weapons) weapon.gameObject.SetActive(false);
            StartCoroutine("EndMotion");
            SelectWeapon(0);
        }

        public void AttackEffect()
        {
            // Player attack; play animation etc...
        }

        public void SelectWeapon(int index)
        {
            if (currentWeaponIndex == index) return;

            weapons[index].gameObject.SetActive(true);
            PlayerDamage = weapons[index].damage;
            Damage.text = PlayerDamage.ToString();
            //Weapon_Image.Instance.changeImage(PlayerDamage);
        }

        public void changeWeapon()
        {
            PlayerDamage=Random.Range(5, 50);
            Weapon_Image.Instance.changeImage(PlayerDamage);
            Damage.text = PlayerDamage.ToString();
        }
        private IEnumerator EndMotion()
        {
            while (true)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
                {
                    StartCoroutine("particleOn");
                    myaudio.PlayOneShot(explosion);
                    anim.ResetTrigger("Attack");
                    anim.SetTrigger("Idle");
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        IEnumerator particleOn()
        {
            particle.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            particle.SetActive(false);
            yield return null;
        }
    }
}