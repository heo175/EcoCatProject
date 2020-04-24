using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public void OpenDialogue()
    {
        GameObject.Find("Canvas").transform.Find("Dialog").gameObject.SetActive(true);
    }
        public void startBtnClick()
    {
        GameObject.Find("Canvas").transform.Find("dwindow").gameObject.SetActive(true);
    }
    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Canvas").transform.Find("Dialog").gameObject.SetActive(true);
        }
    }
    */
}
