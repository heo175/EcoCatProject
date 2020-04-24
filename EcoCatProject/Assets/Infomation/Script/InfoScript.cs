using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScript : MonoBehaviour
{
    public GameObject info1;
    public GameObject info2;
    public GameObject info3;
    public GameObject info4;
    public GameObject info5;
    public GameObject info6;

    int n; // 랜덤값

    // Start is called before the first frame update
    void Start()
    {
        // 랜덤값 생성
        n = Random.Range(0, 9);
        nSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void nSetting() {
        if (n == 0) {
            info1.SetActive(true);
            info2.SetActive(false);
            info3.SetActive(false);
            info4.SetActive(true);
            info5.SetActive(false);
            info6.SetActive(false);
        }else if (n == 1)
        {
            info1.SetActive(true);
            info2.SetActive(false);
            info3.SetActive(false);
            info4.SetActive(false);
            info5.SetActive(true);
            info6.SetActive(false);
        }
        else if (n == 2)
        {
            info1.SetActive(true);
            info2.SetActive(false);
            info3.SetActive(false);
            info4.SetActive(false);
            info5.SetActive(false);
            info6.SetActive(true);
        }
        else if (n == 3)
        {
            info1.SetActive(false);
            info2.SetActive(true);
            info3.SetActive(false);
            info4.SetActive(true);
            info5.SetActive(false);
            info6.SetActive(false);
        }
        else if (n == 4)
        {
            info1.SetActive(false);
            info2.SetActive(true);
            info3.SetActive(false);
            info4.SetActive(false);
            info5.SetActive(true);
            info6.SetActive(false);
        }
        else if (n == 5)
        {
            info1.SetActive(false);
            info2.SetActive(true);
            info3.SetActive(false);
            info4.SetActive(false);
            info5.SetActive(false);
            info6.SetActive(true);
        }
        else if (n == 6)
        {
            info1.SetActive(false);
            info2.SetActive(false);
            info3.SetActive(true);
            info4.SetActive(true);
            info5.SetActive(false);
            info6.SetActive(false);
        }
        else if (n == 7)
        {
            info1.SetActive(false);
            info2.SetActive(false);
            info3.SetActive(true);
            info4.SetActive(false);
            info5.SetActive(true);
            info6.SetActive(false);
        }
        else if (n == 8)
        {
            info1.SetActive(false);
            info2.SetActive(false);
            info3.SetActive(true);
            info4.SetActive(false);
            info5.SetActive(false);
            info6.SetActive(true);
        }
    }
}
