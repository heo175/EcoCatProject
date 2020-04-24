using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{
    public GameObject explainPopup;
    bool Popup;
    //public string SceneName;

    void Start()
    {
        explainPopup.SetActive(false);
        Popup = false;

    }
    public void ExplainPopupOpen()
    {
        explainPopup.SetActive(true);
        Popup = true;
    }

    public void ExplainPopupClose()
    {
        explainPopup.SetActive(false);
        Popup = false;
    }

    void Update() {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if(Popup == true)
                {
                    explainPopup.SetActive(false);
                }
                else
                    SceneManager.LoadScene("GameList");
            }
        }
    }

    public void StartEasy()
    {
        SceneManager.LoadScene("EASYGame");
    }
         public void StartNormal()
    {
        SceneManager.LoadScene("NORMALGame");
    }
         public void StartHard()
    {
        SceneManager.LoadScene("HARDGame");
    }


    public void SeparateGameEasy()
    {
        SceneManager.LoadScene("EASYseparate");
    }
    public void SeparateGameNormal()
    {
        SceneManager.LoadScene("NORMALseparate");
    }
    public void SeparateGameHard()
    {
        SceneManager.LoadScene("HARDseparate");
    }


    public void GrowingTreeGameEasy()
    {
        SceneManager.LoadScene("EASYGrowingTree");
    }
    public void GrowingTreeGameNormal()
    {
        SceneManager.LoadScene("NORMALGrowingTree");
    }
    public void GrowingTreeGameHard()
    {
        SceneManager.LoadScene("HARDGrowingTree");
    }


    public void PickingGameEasy()
    {
        SceneManager.LoadScene("EASYPickingRiverTrash");
    }
    public void PickingGameNormal()
    {
        SceneManager.LoadScene("NORMALPickingRiverTrash");
    }
    public void PickingGameHard()
    {
        SceneManager.LoadScene("HARDPickingRiverTrash");
    }


    public void RunGameEasy()
    {
        SceneManager.LoadScene("EASYRunGame");
    }
    public void RunGameNormal()
    {
        SceneManager.LoadScene("NORMALRunGame");
    }
    public void RunGameHard()
    {
        SceneManager.LoadScene("HARDRunGame");
    }


    public void CatchGameEasy()
    {
        SceneManager.LoadScene("EASYCatchGame");
    }
    public void CatchGameNormal()
    {
        SceneManager.LoadScene("NORMALCatchGame");
    }
    public void CatchGameHard()
    {
        SceneManager.LoadScene("HARDCatchGame");
    }

    public void GameList() {
        SceneManager.LoadScene("GameList");
    }

}
