using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class skipScene : MonoBehaviour
{
    public string SkipScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Skipscene()
    {
        SceneManager.LoadScene(SkipScene);
    }
}
