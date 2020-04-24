using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wateringAnimation : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AnimationActive()
    {
        GetComponent<Animator>().SetTrigger("watering_t");
    }
}
