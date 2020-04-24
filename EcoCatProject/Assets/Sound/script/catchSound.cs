using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catchSound : MonoBehaviour
{
    AudioSource musicPlayer;
    public AudioClip trash_o;
    public AudioClip trash_x;
    public AudioClip heart;
    public AudioClip start;

    public static catchSound instance;
    // Start is called before the first frame update

    void Awake()
    {
        if (catchSound.instance == null)
        {
            catchSound.instance = this;
        }
    }

    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
    }

    public void Trash_O()
    {
        musicPlayer.PlayOneShot(trash_o);
    }

    public void Trash_X()
    {
        musicPlayer.PlayOneShot(trash_x);
    }

    public void Heart()
    {
        musicPlayer.PlayOneShot(heart);
    }

    public void StartBtn()
    {
        musicPlayer.PlayOneShot(start);
    }
}
