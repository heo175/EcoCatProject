using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GrowingTree : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    public float done ;
    public Text timeText;
    public int MinTouch1;
    public int MaxTouch1;
    public int MinTouch2;
    public int MaxTouch2;
    public int MinTouch3;
    public int MaxTouch3;
    public int MinTouch4;
    public int MaxTouch4;

    public Text timer;

    public GameObject background1;
    public GameObject background2;
    public GameObject shovel;
    public GameObject seed;

    public GameObject seed2;
    public GameObject wateringpot;
    public GameObject tree0;

    public GameObject sun;
    public GameObject sunlight;
    public GameObject tree1;

    public GameObject weeds;
    public GameObject tree2;

    public GameObject tree3;



    public Text shovelText;
    public Text waterText;
    public Text sunText;
    public Text weedsText;

    public Text step1;
    public Text step2;
    public Text step3;
    public Text step4;
    public Text step5; //성공

    bool seedCount = false;

    int shoveln;
    int watern;
    int sunn;
    int weedsn ;
                  
    int shovelNum;
    int WaterNum;
    int SunNum;
    int weedsNum ;


    bool isClear = false; // ws = true 이면, 클리어로 넘어갈 수 있게 함
    int n;
    int a = 0; // a=1 이면, wateringpot, sun 을 보이게 함
    int b = 0; // b=1 이면, soilClick 실행

    public string stage;

    public void Start()
    {
 
        shoveln = 0;
        watern = 0;
        sunn = 0;
        weedsn = 0;
        background1.gameObject.SetActive(true);
        background2.gameObject.SetActive(false);
        step1.gameObject.SetActive(true);
        step2.gameObject.SetActive(false);
        step3.gameObject.SetActive(false);
        step4.gameObject.SetActive(false);
        step5.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        seed2.SetActive(false);
        shovel.SetActive(true);
        weeds.SetActive(false);
        if (wateringpot != null) wateringpot.SetActive(false);
        if (sun != null) sun.SetActive(false);

        if (tree0 != null) tree0.SetActive(false);
        if (tree1 != null) tree1.SetActive(false);
        if (tree2 != null) tree2.SetActive(false);
        if (tree3 != null) tree3.SetActive(false);

        shovelNum = Random.Range(MinTouch1, MaxTouch1);
        WaterNum = Random.Range(MinTouch2, MaxTouch2);
        SunNum = Random.Range(MinTouch2, MaxTouch2);
        weedsNum = Random.Range(MinTouch3, MaxTouch3);
        step1.text = "Step1\n모종삽으로 흙을 " + shovelNum + "번 파고, 씨앗을 심어주세요";
        step2.text = "Step2\n물뿌리개를 " + WaterNum + "번 주세요";
        step3.text = "Step3\n햇볕을 " + SunNum + "번 주세요";
        step4.text = "Step4\n잡초를 "+weedsNum+ "번 눌러서 없애세요";

        n = Random.Range(0, 3); // 랜덤값 생성
        Debug.Log(n);

        shovelText.text = "";
        sunText.text = "";
        waterText.text = "";
        weedsText.text = "";
    }

    public void Update()
    {
        if (done > 0)
        {
            done = done - Time.deltaTime;
            timeText.text = ""+(Mathf.Round(done).ToString());
        }

        if(shovelNum <= shoveln)
        {
            shovelText.gameObject.SetActive(false);
            shovel.SetActive(false);
            seed.SetActive(true);
            background1.gameObject.SetActive(false);
            background2.gameObject.SetActive(true);
            
            if(seedCount == true)
            {
                step1.gameObject.SetActive(false);
                step2.gameObject.SetActive(true);
                seed2.SetActive(true);
                seed.SetActive(false);
                wateringpot.SetActive(true);
                waterText.gameObject.SetActive(true);
                
               if(watern >= 5)
               {
                    background1.gameObject.SetActive(true);
                    background2.gameObject.SetActive(false);
                    seed2.SetActive(false);
                    tree0.SetActive(true);

                    if(watern >= WaterNum)
                    {
                        waterText.gameObject.SetActive(false);
                        sunText.gameObject.SetActive(true);
                        tree0.SetActive(false);
                        tree1.SetActive(true);
                        sun.SetActive(true);
                        wateringpot.SetActive(false);
                        step2.gameObject.SetActive(false);
                        step3.gameObject.SetActive(true);
                        
                        if(SunNum <= sunn)
                        {
                            sunText.gameObject.SetActive(false);
                            weedsText.gameObject.SetActive(true);
                            tree1.SetActive(false);
                            tree2.SetActive(true);
                            step3.gameObject.SetActive(false);
                            step4.gameObject.SetActive(true);
                            sun.SetActive(false);
                            weeds.SetActive(true);

                            if(weedsNum <= weedsn)
                            {
                                isClear = true;
                                weeds.SetActive(false);
                                step4.gameObject.SetActive(false);
                                step5.gameObject.SetActive(true);
                                tree2.SetActive(false);
                                tree3.SetActive(true);
                                StartCoroutine(goClearscene());  
                            }
                        }
                    }
               }

            }
        
        }


         /*
        if (seedCount == true)
        {//씨앗만 심은상태, 흙도 덮어야함
            shovelText.gameObject.SetActive(true);
            seed.SetActive(true);

            if (shovelNum <= shoveln)
            {
                shovelText.gameObject.SetActive(false);
                waterText.gameObject.SetActive(true);
                seed2.SetActive(true);
                step1.gameObject.SetActive(false);
                step2.gameObject.SetActive(true);
                shovel.SetActive(false);
                background1.gameObject.SetActive(false);
                background2.gameObject.SetActive(true);
                wateringpot.SetActive(true);
            }

            if (shovelNum <= shoveln && watern >= 5)
            {
                tree0.SetActive(true);
            }

            if (shovelNum <= shoveln && watern >= WaterNum)
            {
                waterText.gameObject.SetActive(false);
                sunText.gameObject.SetActive(true);
                tree0.SetActive(false);
                tree1.SetActive(true);
                sun.SetActive(true);
                wateringpot.SetActive(false);
                step2.gameObject.SetActive(false);
                step3.gameObject.SetActive(true);
            }
            if (SunNum <= sunn)
            {
                sunText.gameObject.SetActive(false);
                weedsText.gameObject.SetActive(true);
                tree1.SetActive(false);
                tree2.SetActive(true);
                step3.gameObject.SetActive(false);
                step4.gameObject.SetActive(true);
                sun.SetActive(false);
                weeds.SetActive(true);
            }
            if (watern >= WaterNum && sunn >= SunNum && shovelNum <= shoveln && weedsNum <= weedsn)
            {
                weeds.SetActive(false);
                step4.gameObject.SetActive(false);
                step5.gameObject.SetActive(true);
                tree2.SetActive(false);
                tree3.SetActive(true);
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
            }
        }
        */
        if (done > 0F)
        {   
            done = done - Time.deltaTime;
            print(Mathf.Ceil(done));
        }

        else if(done <= 0F && isClear == false)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("FailScene");
        }
    }

    IEnumerator goClearscene() {
        Time.timeScale = 1f;
        //SceneManager.LoadScene("ClearScene");

        if (stage == "easy")
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("ClearScene");
        }
        else if (stage == "normal")
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("ClearSceneNORMAL");
        }
        else if (stage == "hard")
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("ClearSceneHARD");
        }
    }

    public void seedClick() {
        seedCount = true;
    }

    public void sunClick()
    {
        sunn++;
        sunText.text = sunn.ToString();
        StartCoroutine("SunShining", 0.2f);
        Invoke("TurnoffSun", 0.2f);
    }
    public void TurnoffSun()
    {
        sun.gameObject.SetActive(true);
        sunlight.gameObject.SetActive(false);
    }
    public void SunShining()
    {
        sun.gameObject.SetActive(false);
        sunlight.gameObject.SetActive(true);
    }

    public void WaterClick() {

        watern++;
        waterText.text = watern.ToString() ;
    }

    public void ShovelNumClick() {
        shoveln++;
        shovelText.text = shoveln.ToString() ;
    }
    public void WeedsClick()
    {
        weedsn++;
        weedsText.text = weedsn.ToString();
    }
}
