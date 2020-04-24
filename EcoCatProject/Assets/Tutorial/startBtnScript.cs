using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startBtnScript : MonoBehaviour
{
    public void startBtnClick()
    {
        GameObject.Find("Canvas").transform.Find("dwindow").gameObject.SetActive(true);
    }
}
