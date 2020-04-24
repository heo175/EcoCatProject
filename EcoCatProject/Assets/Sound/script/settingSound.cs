using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void soundOn()
    {
        AudioListener.pause = false;
    }

    public void soundOff()
    {
        AudioListener.pause = true;
    }
}
