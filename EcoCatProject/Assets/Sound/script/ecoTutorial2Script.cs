using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ecoTutorial2Script : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GetComponent<AudioSource>().Play();

    }

    public void Update()
    {

        if (SceneManager.GetActiveScene().name == "PolarStart")
        {
            Destroy(gameObject);
        }

    }
}
