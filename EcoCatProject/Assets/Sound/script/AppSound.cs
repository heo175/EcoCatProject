using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppSound : MonoBehaviour
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
    }
}
