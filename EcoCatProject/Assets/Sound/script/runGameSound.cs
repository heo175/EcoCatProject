using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runGameSound : MonoBehaviour
{
    AudioSource musicPlayer;
    public AudioClip jumpsound;
    public AudioClip blocksound;
    public AudioClip trashsound;
    public AudioClip heartsound;
    public AudioClip finishsound;

    public static runGameSound instance;
    // Start is called before the first frame update

    void Awake()
    {
        if (runGameSound.instance == null)
        {
            runGameSound.instance = this;
        }
    }

    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
    }

    public void Jump()
    {

        musicPlayer.PlayOneShot(jumpsound);
    }

    public void Block()
    {

        musicPlayer.PlayOneShot(blocksound);
    }

    public void Trash()
    {

        musicPlayer.PlayOneShot(trashsound);
    }

    public void Heart()
    {

        musicPlayer.PlayOneShot(heartsound);
    }

    public void Finish()
    {

        musicPlayer.PlayOneShot(finishsound);
    }
}
