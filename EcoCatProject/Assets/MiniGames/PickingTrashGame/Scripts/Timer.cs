using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float done = 10f;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (done > 0) {
            done = done - Time.deltaTime;
            timeText.text = Mathf.Round(done).ToString();
        }
    }
}
