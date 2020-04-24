using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ecoTutorialSound : MonoBehaviour
{
    // Start is called before the first frame update
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GetComponent<AudioSource>().Play();

    }

    public void Update()
    {

        if (SceneManager.GetActiveScene().name == "Tutorial3")
        {
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "PolarStart")
        {
            Destroy(gameObject);
        }

    }
}
