using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour {

    public AudioClip ExplosionClip;
    public AudioClip CoinGetClip;
    public AudioClip HPrecoverClip;
    AudioSource ExplosionSource;
    AudioSource CoinGetSource;
    AudioSource HPrecoverSource;

    public static SoundManager soundManager;

    private void Awake()
    {
        if(SoundManager.soundManager == null)
        {
            SoundManager.soundManager = this;
            //이 객체가 존재하지 않을 경우, 이 소스코드가 붙어있는 GameObejcet를 받아옵니다.
        }
    }
    void Start () {
        ExplosionSource = GetComponent<AudioSource>();
        HPrecoverSource = GetComponent<AudioSource>();
        CoinGetSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	public void PlayExplosionSound() {
        ExplosionSource.PlayOneShot(ExplosionClip);
	}
    public void PlayCoinGetSound()
    {
        CoinGetSource.PlayOneShot(CoinGetClip);
    }
    public void PlayHPrecoverSound()
    {
        HPrecoverSource.PlayOneShot(HPrecoverClip);
    }
}
