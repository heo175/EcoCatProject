using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Catch_Score : MonoBehaviour
{
    public Catch_GameController gameController;
    public Text lifeText;
    public Text ScoreText;
    private int TrashScore;
    public int life = 5;
    public int score = 0;
    public int goal;
    public GUIText lifeDisplay;
    private int Trashtype;
    public GameObject Can;
    public GameObject Plastic;
    public GameObject Paper;
    public string stage;

    void trashcheck()
    {
        Trashtype = Random.Range(0, 3);
        if (Trashtype == 0)
        {
            Can.SetActive(true);
        }
        else if (Trashtype == 1)
        {
            Plastic.SetActive(true);
        }
        else if (Trashtype == 2)
        {
            Paper.SetActive(true);
        }
    }

    void Start()
    {
        trashcheck();
        UpdateLife();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Trashtype == 0)//캔류
        {
            if (other.transform.name == "Can1(Clone)"|| other.transform.name == "Can2(Clone)" || other.transform.name == "Can3(Clone)")
            {
                catchSound.instance.Trash_O();

                if (score < (goal - 1))
                {
                    score++;
                    UpdateScore();
                }
                else if (score == (goal - 1))
                {
                    if (stage == "easy")
                    {
                        SceneManager.LoadScene("ClearScene");
                    }
                    else if (stage == "normal") {
                        SceneManager.LoadScene("ClearSceneNORMAL");
                    }
                    else if (stage == "hard")
                    {
                        SceneManager.LoadScene("ClearSceneHARD");
                    }

                }
            }
            else if (other.transform.name == "Heart(Clone)")
            {
                catchSound.instance.Heart();
                life += 1;
                UpdateLife();
            }
            else if (!((other.transform.name == "Can1(Clone)" || other.transform.name == "Can2(Clone)" || other.transform.name == "Can3(Clone)")))
            {
                if (life > 1)
                {
                    catchSound.instance.Trash_X();
                    Debug.Log("캔류");
                    life -= 1;
                    Debug.Log("남은 체력" + life);
                    UpdateLife();
                }
                else if (life == 1)
                {
                    catchSound.instance.Trash_X();
                    SceneManager.LoadScene("FailScene");
                }
            }

        }
        else if (Trashtype == 1)//플라스틱류
        {
            if ((other.transform.name == "Plastic1(Clone)" || other.transform.name == "Plastic2(Clone)" || other.transform.name == "Plastic3(Clone)"))
            {
                catchSound.instance.Trash_O();
                if (score < (goal - 1))
                {
                    score++;
                    UpdateScore();
                }
                else if (score == (goal - 1))
                {
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
            else if (other.transform.name == "Heart(Clone)")
            {
                catchSound.instance.Heart();
                life += 1;
                UpdateLife();
            }
            else if (!(other.transform.name == "Plastic1(Clone)" || other.transform.name == "Plastic2(Clone)" || other.transform.name == "Plastic3(Clone)"))
            {
                if (life > 1)
                {
                    catchSound.instance.Trash_X();
                    Debug.Log("플라스틱아님");
                    life -= 1;
                    Debug.Log("남은 체력" + life);
                    UpdateLife();
                }
                else if (life == 1)
                {
                    catchSound.instance.Trash_X();
                    SceneManager.LoadScene("FailScene");
                }
            }

        }
        else if (Trashtype == 2)//종이
        {
            if ((other.transform.name == "Paper1(Clone)"|| other.transform.name == "Paper2(Clone)" || other.transform.name == "Paper3(Clone)"))
            {
                catchSound.instance.Trash_O();
                if (score < (goal - 1))
                {
                    score++;
                    UpdateScore();
                }
                else if (score == (goal-1))
                {
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
            else if (other.transform.name == "Heart(Clone)")
            {
                catchSound.instance.Heart();
                life += 1;
                UpdateLife();
            }
            else if (!(other.transform.name == "Paper1(Clone)" || other.transform.name == "Paper2(Clone)" || other.transform.name == "Paper3(Clone)"))
            {
                if (life > 1)
                {
                    catchSound.instance.Trash_X();
                    Debug.Log("종이아님");
                    life -= 1;
                    Debug.Log("남은 체력" + life);
                    UpdateLife();
                }
                else if (life == 1)
                {
                    catchSound.instance.Trash_X();
                    SceneManager.LoadScene("FailScene");
                }
            }

            }
        }

    void UpdateLife()
        {
            lifeText.text = "X " + life;
        }

        void UpdateScore()
        {
            ScoreText.text = score + " / "+ goal;
        }


    
}