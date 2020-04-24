using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diggingAnimation : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Dig()
    {
        GetComponent<Animator>().SetTrigger("shovel_t");
    }
}
