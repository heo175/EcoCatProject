using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;

    Animator animator;

    public GameObject plasticbag;
    public GameObject book;
    public GameObject cokecan;

    float jumpForce = 780.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float threshold = 0.2f;

    int n; // 랜덤값

    public string stage;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.rigid2D = GetComponent<Rigidbody2D>();

        // 랜덤값 생성
        n = Random.Range(0, 3);
        Debug.Log(n);

        if (n == 0) // 랜덤값이 0이면 플라스틱 -> 비닐봉지 표시
        {
            plasticbag.SetActive(true);
            book.SetActive(false);
            cokecan.SetActive(false);
        }
        else if (n == 1) // 랜덤값이 1이면 종이 -> 책 표시
        {
            plasticbag.SetActive(false);
            book.SetActive(true);
            cokecan.SetActive(false);
        }
        else if (n == 2)  // 랜덤값이 2이면 캔 -> 콜라캔 표시
        {
            plasticbag.SetActive(false);
            book.SetActive(false);
            cokecan.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 점프
        
        if (Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0)
        {
            runGameSound.instance.Jump();
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }
  /*      
        // 빌드 전에 없애야해요
        if (Input.GetKeyDown(KeyCode.Space)) {

            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }
*/
        // 좌우 이동
        int key = 0;
        
        if (Input.acceleration.x > this.threshold) key = 1;
        if (Input.acceleration.x < -this.threshold) key = -1;
  /*    
        // 빌드 전에 없애야해요
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;
*/
        // 플레이어 속도
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // 스피드 제한
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // 움직이는 방향에 따라 이미지 반전
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // 화면 밖으로 나가면 다시 원점으로
        if (transform.position.y < -10)
        {
            runGameSound.instance.Block();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // SceneManager.LoadScene("EASYseparate");
        }

        this.animator.speed = speedx / 2.0f;
    }

    // 도착
    void OnTriggerEnter2D(Collider2D other)
    {
        if (n == 0)
        { // 플라스틱 버려야함
            if (other.transform.name == "plasticwastebin")
            {
                runGameSound.instance.Finish();
                // SceneManager.LoadScene("ClearScene");
                if (stage == "easy")
                {
                    SceneManager.LoadScene("ClearScene");
                }
                else if (stage == "normal")
                {
                    SceneManager.LoadScene("ClearSceneNORMAL");
                }
                else if (stage == "hard")
                {
                    SceneManager.LoadScene("ClearSceneHARD");
                }
                // Debug.Log("플라스틱 성공");
            }
            else
            {
                runGameSound.instance.Block();
                SceneManager.LoadScene("FailScene");
                // Debug.Log("캔, 종이 실패");
            }
        }

        if (n == 1) // 종이 버려야함
        {
            if (other.transform.name == "paperwastebin")
            {
                runGameSound.instance.Finish();
                // SceneManager.LoadScene("ClearScene");
                if (stage == "easy")
                {
                    SceneManager.LoadScene("ClearScene");
                }
                else if (stage == "normal")
                {
                    SceneManager.LoadScene("ClearSceneNORMAL");
                }
                else if (stage == "hard")
                {
                    SceneManager.LoadScene("ClearSceneHARD");
                }
                // Debug.Log("종이 성공");
            }
            else
            {
                runGameSound.instance.Block();
                SceneManager.LoadScene("FailScene");
                // Debug.Log("캔, 플라스틱 실패");
            }
        }

        if (n == 2) // 캔 버려야함
        {
            if (other.transform.name == "canwastebin")
            {
                runGameSound.instance.Finish();
                // SceneManager.LoadScene("ClearScene");
                if (stage == "easy")
                {
                    SceneManager.LoadScene("ClearScene");
                }
                else if (stage == "normal")
                {
                    SceneManager.LoadScene("ClearSceneNORMAL");
                }
                else if (stage == "hard")
                {
                    SceneManager.LoadScene("ClearSceneHARD");
                }
                // Debug.Log("캔 성공");
            }
            else
            {
                runGameSound.instance.Block();
                SceneManager.LoadScene("FailScene");
                // Debug.Log("플라스틱, 종이 실패");
            }
        }


        if (other.transform.name == "loseCollider")
        {
            runGameSound.instance.Block();
            SceneManager.LoadScene("FailScene");
        }
    }
}