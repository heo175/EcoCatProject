using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {

    public float Speed = 1f;
    public float JumpPower = 1f;
    private Animator ani;

    private void Start()
    {
        ani = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Left();
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            ani.SetBool("isMoving", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            ani.SetBool("isMoving", false);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Right();
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            ani.SetBool("isMoving", true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            ani.SetBool("isMoving", false);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
    }

    public void Left()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
    public void Right()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }
    public void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * JumpPower, ForceMode2D.Impulse);
    }
}
