using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightScript : MonoBehaviour
{
    AudioSource musicPlayer;
    public AudioClip music;
    public AudioClip music2;

    public static lightScript instance;
    // Start is called before the first frame update

    void Awake()
    {
        if (lightScript.instance == null) {
            lightScript.instance = this;
        }
    }

    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
    }

    public void PlaySound() {

        musicPlayer.PlayOneShot(music);
    }

    public void PlaySound2()
    {

        musicPlayer.PlayOneShot(music2);
    }
}
