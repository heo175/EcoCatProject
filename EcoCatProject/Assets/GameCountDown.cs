using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class GameCountDown : MonoBehaviour
{

    private int Timer = 0;

    public GameObject background;
    public GameObject Num_3;   //1번
    public GameObject Num_2;   //2번
    public GameObject Num_1;   //3번
    public GameObject Num_GO;
    public GameObject trash;
    void Start()

    {


        //시작시 카운트 다운 초기화
        Timer = 0;
        trash.SetActive(false);
        Num_3.SetActive(false);
        Num_2.SetActive(false);
        Num_1.SetActive(false);
        Num_GO.SetActive(false);
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0)))  //버튼을 누름.
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {  //UI이 위가 아니면.
                StartCoroutine("CheckButtonDownSec");
            }
        }

        //게임 시작시 정지
        if (Timer == 0)
        {
            Time.timeScale = 0.0f;
        }


        //Timer 가 90보다 작거나 같을경우 Timer 계속증가
        if (Timer <= 90)
        {   Timer++;
            // Timer가 30보다 작을경우 3번켜기
            if (Timer < 30)
            {
                Num_3.SetActive(true);
            }

            // Timer가 30보다 클경우 3번끄고 2번켜기
            if (Timer > 30)
            {
                Num_3.SetActive(false);
                Num_2.SetActive(true);
            }

            // Timer가 60보다 작을경우 2번끄고 1번켜기
            if (Timer > 60)
            {
                Num_2.SetActive(false);
                Num_1.SetActive(true);
            }

            //Timer 가 90보다 크거나 같을경우 1번끄고 GO 켜기 LoadingEnd () 코루틴호출 
            if (Timer >= 90)
            {
                Num_1.SetActive(false);
                Num_GO.SetActive(true);
                StartCoroutine(this.LoadingEnd());
                Time.timeScale = 1.0f; //게임시작
            }
        }
    }

    IEnumerator LoadingEnd()
    {
        yield return new WaitForSeconds(0.7f);
        Num_GO.SetActive(false);
        background.SetActive(false);
        trash.SetActive(true);
    }

}
