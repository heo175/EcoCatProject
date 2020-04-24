using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SASettingScripts : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)

        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ChangeScene0()
    {
        SceneManager.LoadScene("SaveAnimal0");
    }
    public void ChangeScene1() {
        SceneManager.LoadScene("SaveAnimal1");
    }

    public void ChangeScene2()
    {
        SceneManager.LoadScene("SaveAnimal2");
    }
    public void ChangeScene3()
    {
        SceneManager.LoadScene("SaveAnimal3");
    }
    public void ChangeScene4()
    {
        SceneManager.LoadScene("SaveAnimal4");
    }
}
