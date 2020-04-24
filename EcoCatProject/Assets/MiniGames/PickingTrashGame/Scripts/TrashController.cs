using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrashController : MonoBehaviour
{
    AudioSource musicPlayer;
    public AudioClip music;

    public GameObject[] TrashSetObj;
    public int TrashNum;
    int TrashTouchNum;
    GameObject can;
    GameObject bottle;
    GameObject petbottle;
    GameObject plasticbag;
    GameObject paper;
    GameObject paper2;
    GameObject paperairplane;
    GameObject can2;
    GameObject plasticdrink;
    GameObject timebar;

    public string stage;


    public float done ;

    // Start is called before the first frame update
    void Start()
    {
        TrashTouchNum = 0;
        for (int i = 0; i < TrashNum; i++) {
            TrashSetObj[i].transform.localPosition += new Vector3(Random.Range(0, 1), Random.Range(0, 3), 0);
        }
        musicPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 터치하면 사라져요
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(touchPosition, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                musicPlayer.Play();
                hit.collider.gameObject.SetActive(false);
                
                TrashTouchNum++;
            }
        }

      //  GameObject director = GameObject.Find("GameDirector");
       
        if (done > 0F)
        {           
            done = done - Time.deltaTime;
            print(Mathf.Ceil(done));
         //   director.GetComponent<GameDirector>().DecreaseTime();

            // 다 누른 상태면 Clear 화면으로 넘어감
            if (TrashTouchNum == TrashNum)
            {
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
            }
        }
        else {
            // print("Time is Over");
            SceneManager.LoadScene("FailScene");
        }
    }


}
