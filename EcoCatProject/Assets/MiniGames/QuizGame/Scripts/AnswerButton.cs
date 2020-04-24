using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
   // AudioSource musicPlayer;
    //public AudioClip music;

    public Text answerText;

    private AnswerData answerData;
    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        //musicPlayer = GetComponent<AudioSource>();

        gameController = FindObjectOfType<GameController>();
    }

    public void Setup(AnswerData data)
    {
        answerData = data;
        answerText.text = answerData.answerText;
    }


    public void HandleClick()
    {
        //musicPlayer.Play();
        gameController.AnswerButtonClicked(answerData.isCorrect);
    }
}