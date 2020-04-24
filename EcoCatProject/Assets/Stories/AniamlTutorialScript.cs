using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using UnityEngine.UI;
using System;

public class AniamlTutorialScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EcoSkip() {
        SceneManager.LoadScene("PolarStart");
    }

    public void PolarBearIntroSkip()
    {
        SceneManager.LoadScene("AppIntro1");
    }

    public void AppIntroSkip()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void PolarBearOutroSkip()
    {
        // animal = 1, grade = 0 으로 만들기 (돌고래 0단계로 만들기)

        SceneManager.LoadScene("PolarClear");
    }

    public void DolphinIntroSkip()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void DolphinOutroSkip()
    {
        SceneManager.LoadScene("PandaStart");
    }

    public void PandaIntroSkip()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void PandaOutroSkip()
    {
        SceneManager.LoadScene("TreeStart");
    }

    public void TreeIntroSkip()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void TreeOutroSkip()
    {
        SceneManager.LoadScene("TreeClear");
    }

}
