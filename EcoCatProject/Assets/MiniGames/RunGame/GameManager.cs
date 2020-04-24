using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public GameObject a;

    public string stage; 

    void Start() {
        Time.timeScale = 1f;
    }
    // Start is called before the first frame update
    public void Win() {
        //SceneManager.LoadScene("ClearScene");
        if (stage == "easy")
        {
            SceneManager.LoadScene("ClearScene");
        }
        else if (stage == "normal")
        {
            SceneManager.LoadScene("ClearSceneNORMAL");
        }
        else if (stage == "hard")
        {
            SceneManager.LoadScene("ClearSceneHARD");
        }
        Debug.Log("Win!");
    }
    public void Lose() {
        SceneManager.LoadScene("FailScene");
        Debug.Log("Lose!");
    }

    public void Stop()
    {
        Time.timeScale = 0f;
        
    }
    //   public void Replay() {
    //       SceneManager.LoadScene("Play");
    //   }
}
