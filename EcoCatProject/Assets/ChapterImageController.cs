using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterImageController : MonoBehaviour
{
    public string Scenename;
    public GameObject ClearMark;
    public bool whatScene;

    // Start is called before the first frame update
    void Start()
    {
        if (whatScene == false)
            Invoke("StartScene", 4);
        else if (whatScene == true)
        {
            Invoke("ClearScene1", 1);
            Invoke("StartScene", 7);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartScene()
    {
        SceneManager.LoadScene(Scenename);
    }
    public void ClearScene1()
    {
        markSound.instance.PlaySound();
        ClearMark.SetActive(true);
    }
}
