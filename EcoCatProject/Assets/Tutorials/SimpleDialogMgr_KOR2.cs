using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Made By 민들레미솔

public class SimpleDialogMgr_KOR2 : MonoBehaviour
{
    //변수모음
    #region Variables
    public GameObject Dialog;//대화창 오브젝트
    public TextAsset DialogFile;//대화창 파일
    public Text theName;//이름 Text 오브젝트
    public Text theDialogue;//대화 Text 오브젝트

    public GameObject theImage;//이미지 오브젝트
    public GameObject nextButton;//스킵버튼 오브젝트
    public GameObject QuitButton;//나가기버튼 오브젝트
    public GameObject[] Choice;//선택문 오브젝트

    public string[] NtextLines;//이름
    public string[] DtextLines;//대사
    public Sprite[] ImageFile;//일러스트 파일 

    public bool useTyping = false;//오토타이핑 쓸껀가
    public float typingSpeed = 0f;//오토타이핑 속도
    private bool isTyping = false;//오토타이핑 효과가 현재 작동중인가
    private bool cancelTyping = false;//오토타이핑 정지

    public int currentLine = 0;//진행중인 대사
    public int endAtLine;//마지막 대사
    public float BwaitTime = 1f;//스킵버튼 시간 

    [System.NonSerialized]
    public bool ContinueChoice = false; //다음 take에도 선택문을 사용할건가
    [System.NonSerialized]
    public int take = 0; //몇번째 선택단계인가
    [System.NonSerialized]
    public int howManyChoice = 0; //선택문 개수

    #endregion

    public void OpenDialogue1()
    {
        Debug.Log("힝야");
        GameObject.Find("Canvas").transform.Find("Dialog").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("FallenEcoClick").gameObject.SetActive(false);

    }

    //스크립트 활성화마다 실행함수
    void OnEnable()
    {
        ReadyDialogue(null);
    }

    //대화창 준비함수
    public void ReadyDialogue(TextAsset DFile2)
    {
        currentLine = 0;//대화상태 초기화       

        if (Choice[0] != null)//선택문을 사용할거면
        {
            howManyChoice = Choice.Length - 1;//배열길이로 초기화

            if (DFile2 != null)//다음 대화창 파일이 안비어있으면
            {
                DialogFile = DFile2;//대화파일을 바꾸고
            }
            else if (take == 0)//첫번째 take면
            {
                ContinueChoice = true;//이번take에 선택문을 사용한다
            }
            if (howManyChoice > take)//마지막take가 아니면
            {
                Choice[take].SetActive(false);
            }
        }

        if (DialogFile != null)//대화창 파일이 안 비었으면
        {
            char[] cut = { '\n', ':' };//자를때 기준이 되는 문자들 
            string[] imsi = DialogFile.text.Split(cut);//파일속 문자열을 적당히 잘라서

            NtextLines = new string[imsi.Length / 2];
            DtextLines = new string[imsi.Length / 2];//이름과 대화의 두가지정보니까 2로 나눈사이즈로 배열크기를 정해준다

            int a = 0, b = 0;//분류용 변수
            for (int n = 0; n < imsi.Length; n++)//내용별로 분류한다
            {
                if (n % 2 == 0)//인덱스번호가 짝수인 정보는
                {
                    NtextLines[a] = imsi[n];//이름배열로
                    a++;
                }
                else//홀수인 정보는
                {
                    DtextLines[b] = imsi[n];//대화배열로 넣어준다
                    b++;
                }
            }
        }

        endAtLine = NtextLines.Length - 1;//배열길이로 초기화
        if (Choice[0] != null && take == 0 && endAtLine == 1)
        {
            Choice[take].SetActive(true);
        }

        if (theImage != null)//일러스트 넣을거면
        {
            theImage.SetActive(false);
        }

        if (useTyping == true)//타이핑효과 쓰면
        {
            nextButton.SetActive(false);
            NextDialogue();
        }
        else//안쓰면
        {
            nextButton.SetActive(true);
            if (QuitButton != null)//나가기버튼을 사용하면
            {
                QuitButton.SetActive(true);
            }
            NextText();
        }
    }

    //다음대화 함수
    #region NextTalk
    void NextDialogue()//타이핑 쓰면
    {
        if (isTyping == false)//오토타이핑 효과가 현재 작동중이지 않으면
        {
            if (theImage != null)//이미지오브젝트를 사용하고
            {
                if (ImageFile[currentLine] != null)//이미지파일이 안비어있으면
                {
                    theImage.GetComponent<Image>().sprite = ImageFile[currentLine];//이미지파일을 이미지오브젝트에 넣고
                    theImage.SetActive(true);//이미지오브젝트 활성화
                }
                else//비어있으면
                {
                    theImage.SetActive(false);//이미지오브젝트 비활성화
                }
            }

            theName.text = NtextLines[currentLine];//이름배열을 이름텍스트오브젝트에 넣자

            if (currentLine == endAtLine)//대화가 끝났으면
            {
                DisableDialog();//비활성화 함수
            }
            else//아니면
            {
                StartCoroutine(TypingEffect(DtextLines[currentLine]));//오토타이핑효과 코루틴 시작
            }

            currentLine += 1;
        }

        else if (isTyping && cancelTyping)//대화가 출력이 끝났으면
        {
            cancelTyping = true;//오토타이핑 정지
        }

        nextButton.SetActive(false);
        if (QuitButton != null)//나가기버튼을 사용하면
        {
            QuitButton.SetActive(false);
        }
    }



    void NextText()//타이핑 안 쓰면
    {
        if (isTyping == false)//오토타이핑 효과가 현재 작동중이지 않으면
        {
            if (theImage != null)//이미지오브젝트를 사용하고
            {
                if (ImageFile[currentLine] != null)//이미지파일이 안비어있으면
                {
                    theImage.GetComponent<Image>().sprite = ImageFile[currentLine];//이미지파일을 이미지오브젝트에 넣고
                    theImage.SetActive(true);//이미지오브젝트 활성화
                }
                else//비어있으면
                {
                    theImage.SetActive(false);//이미지오브젝트 비활성화
                }
            }

            theName.text = NtextLines[currentLine];//이름배열을 이름텍스트오브젝트에 넣자
            theDialogue.text = DtextLines[currentLine];//대화배열도 대화텍스트오브젝트에 넣자

            currentLine += 1;//진행변수 업데이트

            if (currentLine > endAtLine)//클때
            {
                DisableDialog();//비활성화 함수
            }
        }

        if (Choice[0] != null)//선택문을 사용할거면
        {
            if (howManyChoice > take)//마지막take가아니면
            {
                if (ContinueChoice == true)//이번take에 선택문을 사용할꺼고
                {
                    if (currentLine == endAtLine)//대화가 끝났으면
                    {
                        nextButton.SetActive(false);
                        Choice[take].SetActive(true);
                    }
                    else//안 끝났으면
                    {
                        nextButton.SetActive(true);
                        Choice[take].SetActive(false);
                    }
                }
                else//이번take에 선택문을 사용안할거면
                {
                    nextButton.SetActive(true);
                    Choice[take].SetActive(false);
                }
            }
            else//마지막take면
            {
                nextButton.SetActive(true);
            }
        }
    }
    #endregion

    //타자효과 함수
    private IEnumerator TypingEffect(string lineOfletter)
    {
        int letter = 0;//몇글자 출력했는지 확인용
        theDialogue.text = "";//실제로 보여지는 부분
        isTyping = true;//오토타이핑 효과가 현재 작동중이다
        cancelTyping = false;//오토타이핑 정지

        while (isTyping && !cancelTyping && (letter < lineOfletter.Length - 1))
        {
            theDialogue.text += lineOfletter[letter];
            letter += 1;
            yield return new WaitForSeconds(typingSpeed);//다음 글자가 출력될때까지의 대기시간(타이핑 속도)
        }

        theDialogue.text = lineOfletter;
        isTyping = false;
        cancelTyping = false;

        if (QuitButton != null)//나가기버튼을 사용하면
        {
            QuitButton.SetActive(true);
        }
        yield return new WaitForSeconds(BwaitTime);//스킵버튼 쿨타임

        if (Choice[0] == null)//선택문을 안쓸거면
        {
            nextButton.SetActive(true);
        }
        else//선택문을 사용할거면
        {
            if (howManyChoice > take)//마지막take가아니면
            {
                if (ContinueChoice == true)//이번take에 선택문을 사용할꺼고
                {
                    if (currentLine == endAtLine)//대화가 끝났으면
                    {
                        nextButton.SetActive(false);
                        Choice[take].SetActive(true);
                    }
                    else//안 끝났으면
                    {
                        nextButton.SetActive(true);
                        Choice[take].SetActive(false);
                    }
                }
                else//이번take에 선택문을 사용안할거면
                {
                    nextButton.SetActive(true);
                    Choice[take].SetActive(false);
                }
            }
            else//마지막take면
            {
                SceneManager.LoadScene("Tutorial3");
            }
        }
    }

    //스킵버튼을 누르면 상태에 따라 알맞은 함수를 실행하는 함수
    public void skipButtonControl()
    {
        if (useTyping == true)
        {
            NextDialogue();
        }
        else
        {
            NextText();
        }
    }

    //활성화 비활성화 함수모음
    #region ActiveDeactive

    public void ChoiceOnOff(bool onoff)//선택문 온오프함수
    {
        take += 1;//진행단계 변수 업데이트 
        ContinueChoice = onoff;//이벤트트리거로 변수를 받아와서 넣는다
    }

    //대화창 활성화 함수
    public void EnableDialog()
    {
        OnEnable();
    }

    //대화창 비활성화 함수
    public void DisableDialog()
    {
        currentLine = 0;//대화상태 초기화

        if (theImage != null)//일러스트 쓰면
        {
            theImage.SetActive(false);
        }

        if (Choice[0] != null)//선택문 활성화 되있으면
        {
            if (howManyChoice > take)//마지막 take가 아니면
            {
                for (int n = 0; n < Choice.Length - 1; n++)
                {
                    Choice[n].SetActive(false);
                }
            }
        }
        StopCoroutine("TypingEffect");//코루틴 종료
        Dialog.SetActive(false);//대화창 비활성화
    }
    #endregion
}