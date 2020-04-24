using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSound : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GetComponent<AudioSource>().Play();

    }

    public void Update()
    {

        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "AppIntro1")
        {
            Destroy(gameObject);
        }

    }
}
