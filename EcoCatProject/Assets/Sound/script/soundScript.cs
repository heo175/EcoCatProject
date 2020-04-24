using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class soundScript : MonoBehaviour
{
    //private AudioSource musicPlayer;
    //public AudioClip backgroundMusic;

    // Start is called before the first frame update
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //musicPlayer = GetComponent<AudioSource>();
        //playSound(backgroundMusic, musicPlayer);
        GetComponent<AudioSource>().Play();

    }

    public void Update()
    {
        
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "Tutorial1")
        {
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "PolarStart")
        {
            Destroy(gameObject);
        }

    }

    /*
    public static void playSound(AudioClip clip, AudioSource audioPlayer) {
        audioPlayer.Stop();
        audioPlayer.clip = clip;
        audioPlayer.loop = true;
        audioPlayer.time = 0;
        audioPlayer.Play();
    }
    */

}
