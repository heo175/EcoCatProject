using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject howtoVuforia;
    bool x = false;
    public void SceneChange1() {
        SceneManager.LoadScene("imsi");
    }

    public void SceneChange2()
    {
        SceneManager.LoadScene("VuforiaScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        howtoVuforia.SetActive(false);
    }

    public void HowtoVuforiaPopup()
    {
        if (howtoVuforia.activeSelf == false)
        {
            howtoVuforia.SetActive(true);
        }
        else if (howtoVuforia.activeSelf == true)
        {
            howtoVuforia.SetActive(false);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (howtoVuforia.activeSelf == true)
                {
                    howtoVuforia.SetActive(false);
                }
                else
                    SceneManager.LoadScene("SearchScene");

            }
        }
    }
}
