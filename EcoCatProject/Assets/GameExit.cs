using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameExit : MonoBehaviour
{
    public GameObject SettingPopup;



    public GameObject ExitPopup;

    // Start is called before the first frame update
    void Start()
    {
        ExitPopup.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (SettingPopup.activeSelf == true)
                {
                    SettingPopup.SetActive(false);
                }
                else if (ExitPopup.activeSelf == false)
                {
                    ExitPopup.SetActive(true);
                }
            }
        }
    }


    public void ExitPopupOpen()
    {
        ExitPopup.SetActive(true);
    }

    public void ExitPopupClose()
    {
        ExitPopup.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
