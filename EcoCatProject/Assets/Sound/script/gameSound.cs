using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameSound : MonoBehaviour
{
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
        GetComponent<AudioSource>().Play();

    }

    public void Update()
    {

        if (SceneManager.GetActiveScene().name == "ClearScene")
        {
            Destroy(gameObject);
            count = 0;
        }

        if (SceneManager.GetActiveScene().name == "ClearSceneNORMAL")
        {
            Destroy(gameObject);
            count = 0;
        }


        if (SceneManager.GetActiveScene().name == "ClearSceneHARD")
        {
            Destroy(gameObject);
            count = 0;
        }

        if (SceneManager.GetActiveScene().name == "FailScene")
        {
            Destroy(gameObject);
            count = 0;
        }

        if (SceneManager.GetActiveScene().name == "GameList")
        {
            Destroy(gameObject);
            count = 0;
        }

    }

    public void stopSound() { 
        GetComponent<AudioSource>().Stop();
    }

    public void replayMusic()
    {
        GetComponent<AudioSource>().Play();
    }

    public void quickBtnClick() {
        Destroy(gameObject);
        count = 0;
    }
}
