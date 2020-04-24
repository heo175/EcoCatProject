using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfSceneController : MonoBehaviour
{

    public GameObject image;
    private int inum = 0;
    // Start is called before the first frame update


    public void Imageview() {
        if (inum == 0)
        {
            image.SetActive(true);
            inum++;
        }
        else if (inum == 1)
        {
            image.SetActive(false);
            inum--;
        }

    }
    // Update is called once per frame

}
