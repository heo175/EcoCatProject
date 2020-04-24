using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSet : MonoBehaviour
{
    public GameObject setCanvas;
    int n = 0; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (n == 0)
        {
           
        }
        else if(n==1)
        {
          
        }
    }


    public void setClick1()
    {
        setCanvas.SetActive(true);
    }

    public void setClick2()
    {
        setCanvas.SetActive(false);
    }
}
