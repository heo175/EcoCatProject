using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoseScene : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
            if (other.transform.name == "LoseCollider")
            {
                    SceneManager.LoadScene("failScene"); 
            }



    }
    
    }
