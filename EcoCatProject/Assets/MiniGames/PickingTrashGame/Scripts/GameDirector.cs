using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject timebar;
    public Text timeText;
    public float done = 10.0F;

    // Start is called before the first frame update
    void Start()
    {
        this.timebar = GameObject.Find("timebar");
    }

    private void Update()
    {
        if (done > 0F)
        {
            done = done - Time.deltaTime;
           // timeText.text = Mathf.Ceil(done).ToString();
        }
        else {
            done = 0;
        }
    }

}
