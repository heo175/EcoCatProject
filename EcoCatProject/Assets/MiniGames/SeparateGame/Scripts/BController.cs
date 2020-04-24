using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 베플리 분리수거하러 벽돌 올라가는 게임
public class BController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    GameObject plasticbag;
    GameObject book;
    GameObject cokecan;

    float jumpForce = 780.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float threshold = 0.2f;
    int n; // 랜덤값
   

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        //  this.animator = GetComponent<Animator>();

        // 랜덤값 생성
        n = Random.Range(0, 3);
        Debug.Log(n);

        plasticbag = GameObject.Find("plasticbag");
        book = GameObject.Find("book");
        cokecan = GameObject.Find("cokecan");

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
        /* 모바일용
        if (Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0) { 
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }
        */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        // 좌우 이동
        int key = 0;
        if (Input.acceleration.x > this.threshold) key = 1;
        if (Input.acceleration.x < -this.threshold) key = -1;

        // 플레이어 속도
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // 스피드 제한
        if (speedx < this.maxWalkSpeed) {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // 움직이는 방향에 따라 반전
       // if (key != 0) {
       //     transform.localScale = new Vector3(key, 1, 1);
       // }

        // 속도에 맞춰 애니메이션 속도 바꿈
      //  this.animator.speed = speedx / 2.0f;

        // 쓰레기통에 닿았을 때

    }

        void OnTriggerEnter2D(Collider2D other) {
        if (n == 0) { // 플라스틱 버려야함
            if (other.transform.name == "plasticwastebin")
            {
                SceneManager.LoadScene("ClearScene");
               // Debug.Log("플라스틱 성공");
            }
            else {
                SceneManager.LoadScene("FailScene");
                // Debug.Log("캔, 종이 실패");
            }
        }

        if (n == 1) // 종이 버려야함
        {
            if (other.transform.name == "paperwastebin")
            {
                SceneManager.LoadScene("ClearScene");
               // Debug.Log("종이 성공");
            }
            else
            {
                SceneManager.LoadScene("FailScene");
                // Debug.Log("캔, 플라스틱 실패");
            }
        }

        if (n == 2) // 캔 버려야함
        {
            if (other.transform.name == "canwastebin")
            {
                SceneManager.LoadScene("ClearScene");
               // Debug.Log("캔 성공");
            }
            else
            {
                SceneManager.LoadScene("FailScene");
                // Debug.Log("플라스틱, 종이 실패");
            }
        }

    }
}
