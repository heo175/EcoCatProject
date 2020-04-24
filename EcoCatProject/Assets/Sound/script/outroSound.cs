using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class outroSound : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GetComponent<AudioSource>().Play();

    }

    public void Update()
    {

        if (SceneManager.GetActiveScene().name == "PolarClear")
        {
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "DolphinClear")
        {
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "PandaClear")
        {
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "TreeClear")
        {
            Destroy(gameObject);
        }

    }
}
