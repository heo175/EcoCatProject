using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkEcoControl : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isWorking", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WalkEco()
    {
        animator.SetBool("isWorking", true);
    }
    public void StopEco()
    {
        animator.SetBool("isWorking", false);

    }
}
