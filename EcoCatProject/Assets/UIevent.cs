using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIevent : MonoBehaviour
{
    private bool pauseOn = false;
    public GameObject pausePanel;

    public void ActivePauseBtn() {
        if (!pauseOn)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
        }
        pauseOn = !pauseOn;
        
    }

    public void RetryBtn()
    {
        Debug.Log("다시시작");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitBtn() {
        Time.timeScale = 1.0f;
        Debug.Log("게임 종료, 목록으로");
        SceneManager.LoadScene("GameList");
    }
}
