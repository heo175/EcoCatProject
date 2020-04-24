using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainSound : MonoBehaviour
{
    AudioSource musicPlayer;
    public AudioClip music;

    private static int count = 0;
    private int index;

    public void Awake()
    {
        index = count;
        count++;
        Debug.Log("awake, " + gameObject.name + ", index is " + index);

        if (index == 0)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            //count = 0;
        }

        musicPlayer = GetComponent<AudioSource>();
        if (!musicPlayer.isPlaying) {
            musicPlayer.Play();
        }
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "CatchMenuScreen")
        {
            Destroy(gameObject);
            count = 0;
        }

        if (SceneManager.GetActiveScene().name == "GrowingMenuScreen")
        {
            Destroy(gameObject);
            count = 0;
        }

        if (SceneManager.GetActiveScene().name == "PickingMenuScreen")
        {
            Destroy(gameObject);
            count = 0;
        }


        if (SceneManager.GetActiveScene().name == "MenuScreen")
        {
            Destroy(gameObject);
            count = 0;
        }

        if (SceneManager.GetActiveScene().name == "RunMenuScreen")
        {
            Destroy(gameObject);
            count = 0;
        }

        if (SceneManager.GetActiveScene().name == "SeparateMenuScreen")
        {
            Destroy(gameObject);
            count = 0;
        }

        if (SceneManager.GetActiveScene().name == "PolarBearOutro")
        {
            Destroy(gameObject);
            count = 0;
        }


        if (SceneManager.GetActiveScene().name == "DolphinOutro")
        {
            Destroy(gameObject);
            count = 0;
        }


        if (SceneManager.GetActiveScene().name == "PandaOutro")
        {
            Destroy(gameObject);
            count = 0;
        }


        if (SceneManager.GetActiveScene().name == "TreeOutro")
        {
            Destroy(gameObject);
            count = 0;
        }

        if (SceneManager.GetActiveScene().name == "VuforiaSceneNormal")
        {
            Destroy(gameObject);
            count = 0;
        }
    }
}
