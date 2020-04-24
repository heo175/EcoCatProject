using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class markSound : MonoBehaviour
{
    AudioSource musicPlayer;
    public AudioClip music;

    public static markSound instance;
    // Start is called before the first frame update

    void Awake()
    {
        if (markSound.instance == null)
        {
            markSound.instance = this;
        }
    }

    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        musicPlayer.PlayOneShot(music);
    }
}
