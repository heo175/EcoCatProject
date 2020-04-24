using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    AudioSource musicPlayer;
    public AudioClip music;

    public Text questionDisplayText;
    public Text lifeDisplayText;
    public Text timeRemainingDisplayText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;
    private int QuestionLevel;
    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;
    public int difficulty;
    private bool isRoundActive;
    private float timeRemaining;
    private int questionIndex;
    private int questionNumber=0;
    int[] QuestionArray;
    public int playerLife;
    
    int difficultynum;
    public Text clearCount;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

//a    public GameObject Canvas;
    public GameObject normallight;
    public GameObject greenlight;
    public GameObject greenbackground;
    public GameObject redlight;
    public GameObject redbackground;
    public GameObject EcoCorrect;
    public GameObject EcoWrong;
    public GameObject EcoDefault;
    // Use this for initialization

    public string stage;

    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        difficultynum = difficulty;
        QuestionArray = new int[difficulty];
        dataController = FindObjectOfType<DataController>();
        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimitInSeconds;
        UpdateTimeRemainingDisplay();

        ClearCountDown();
        questionIndex = Random.Range(0, 20);
        QuestionArray[questionNumber] = questionIndex;
        ShowQuestion();
        isRoundActive = true;

        normallight.SetActive(true);
        greenlight.SetActive(false);
        redlight.SetActive(false);
        greenbackground.SetActive(false);
        redbackground.SetActive(false);
        EcoDefault.SetActive(true);
        EcoWrong.SetActive(false);
        EcoCorrect.SetActive(false);
    }

    private void MatchQuestion()
    {
        while (true)
        {
            int check = 0;
            questionIndex = Random.Range(0, 20);
            for (int i = 0; i < questionNumber; i++)
            {
                if (questionIndex != QuestionArray[i])
                    check++;
            }
            if (check == (questionNumber))
            {
                QuestionArray[questionNumber] = questionIndex;
                break;
            }
        }
    }


    private void ShowQuestion()
    {

        RemoveAnswerButtons();
        MatchQuestion();
        QuestionData questionData = questionPool[QuestionArray[questionNumber]];
        questionDisplayText.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]);
        }
    }

    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if (!isCorrect)
        {
            playerLife -= 1;
            lifeDisplayText.text = "X " + playerLife.ToString();
            Debug.Log("틀림");
            StartCoroutine("WrongAnswer", 2f);
            if (playerLife <= 0)
            {
                SceneManager.LoadScene("FailScene");
            }

        }
        else if(isCorrect)
        {
            questionNumber++;
            difficultynum--;
            StartCoroutine("CorrectAnswer", 2f);
            ClearCountDown();
            Debug.Log("맞음");

            if (questionNumber < (difficulty-1))
            {
                NextQuestion();
            }

            else if (questionNumber >= (difficulty-1))
            {
                //SceneManager.LoadScene("ClearScene");
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
        NextQuestion();
    }

    public void ClearCountDown()
    {
        clearCount.text = (difficultynum-1) + "문제";
    }
    public void NextQuestion(){
       Invoke("NormalLight", 0.3f);
       questionIndex++;
       Debug.Log("질문번호: " + questionNumber);
       ShowQuestion();
       musicPlayer.Play();
    }
    public void CorrectAnswer()
    {
        normallight.SetActive(false);
        redlight.SetActive(false);
        greenlight.SetActive(true);
        greenbackground.SetActive(true);
        EcoDefault.SetActive(false);
        EcoWrong.SetActive(false);
        EcoCorrect.SetActive(true);

        lightScript.instance.PlaySound();
    }
    public void WrongAnswer()
    {
        normallight.SetActive(false);
        greenlight.SetActive(false);
        redlight.SetActive(true);
        redbackground.SetActive(true);
        EcoDefault.SetActive(false);
        EcoWrong.SetActive(true);
        EcoCorrect.SetActive(false);

        lightScript.instance.PlaySound2();
    }
    public void NormalLight()
    {
        normallight.SetActive(true);
        greenlight.SetActive(false);
        redlight.SetActive(false);
        greenbackground.SetActive(false);
        redbackground.SetActive(false);
        EcoDefault.SetActive(true);
        EcoWrong.SetActive(false);
        EcoCorrect.SetActive(false);
    }
    public void EndRound()
    {
        isRoundActive = false;
        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    private void UpdateTimeRemainingDisplay()
    {
        timeRemainingDisplayText.text = "" + Mathf.Round(timeRemaining).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRoundActive)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeRemainingDisplay();
            if (timeRemaining <= 0f)
            {
                SceneManager.LoadScene("FailScene");
            }
        }
    }
}