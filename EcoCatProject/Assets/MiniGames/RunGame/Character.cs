using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    // Update is called once per frame
    public float moveSpeed = 2f;
    public int goal;
    int score;
    public Text Score;
    public GameObject ScoreDisplay;
    public GameObject ResultSuccess;
    int scoreCount = 0;
    public float jumpPower1 = 600f;
    public GameManager gameManager;
    bool isJumping = false;
    bool isPowerJumping = false;
    public int life;
    public Text CountText;
    SpriteRenderer spriteRenderer;
    bool isUnBeatTime;
    void Start()
    {
        isUnBeatTime = false; 
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

    }

    public void Jump() {
        Debug.Log("점프파워: " + jumpPower1);
        if (isJumping == false) {

            GetComponent<Animator>().SetTrigger("Jump_t");
            isJumping = true;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpPower1);
            runGameSound.instance.Jump();
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.transform.name == "Groundunit") {
            isJumping = false;
        }
        else if (col.transform.name == "LastGroundunit")
        {
            isJumping = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.transform.name == "Endpoint")
        {
            runGameSound.instance.Finish();

            if (score >= goal)
                gameManager.Win();
            else if (score < goal)
                gameManager.Lose();
        }

        else if (col.transform.name == "LoseCollider" && isUnBeatTime == false)
        {
            runGameSound.instance.Block();
            Destroy(col.gameObject);
            if (life == 1)
            {
                life -= 1;
                gameManager.Lose();
            }
            else
            {
                life -= 1;
                UpdateLife();
                Debug.Log("남은 체력 : " + life);
                StartCoroutine("UnBeatTime");
            }
        }
        else if (col.transform.name == "Bonus")
        {
            runGameSound.instance.Heart();
            Destroy(col.gameObject);
            life += 1;
            UpdateLife();
        }
        else if (col.transform.name == "Trash")
        {
            runGameSound.instance.Trash();
            Destroy(col.gameObject);
            score++;
            UpdateScore();
            if (score >= goal)
            {
                ScoreDisplay.SetActive(false);
                ResultSuccess.SetActive(true);
            }
        }
        else if (col.transform.name == "Die")
        {
            gameManager.Lose();
        }

        if (col.transform.name == "Jumping")
        {
            isPowerJumping = true;
            jumpPower1 *= 1.5f;
        }
        else if (col.transform.name != "Jumping")
        {
            isPowerJumping = false;
            jumpPower1 = 600f;
        }
    }

    IEnumerator UnBeatTime()
    {
        int countTime = 0;
        isUnBeatTime = true;
        while (countTime < 7) {
            if (countTime % 2 == 0)
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            else
                spriteRenderer.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        isUnBeatTime = false;
        yield return null;
    }
    void UpdateLife()
    {
        if (CountText != null)
            CountText.text = "X " + life;

    }

    void UpdateScore() {
      
            Score.text = score + " / " + goal;
        }
    }
