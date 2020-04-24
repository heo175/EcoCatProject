using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Catch_GameController : MonoBehaviour {
	
	public Camera cam;
	public GameObject[] balls;
	public float timeLeft;
	public Text timerText;
    public GameObject Score;
    public GameObject FinalScore;
    public GameObject Timer;
	public GameObject startButton;
	public Catch_TrashcanController TrashcanController;
	private float maxWidth;
	private bool counting;
	
	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = balls[0].GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;
		timerText.text = "" + Mathf.RoundToInt (timeLeft);
	}
    void Update() {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Debug.Log("옹?");
                SceneManager.LoadScene("MainScene");
            }
        }

    }
    public void FixedUpdate () {
		if (counting) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				timeLeft = 0;
			}
			timerText.text = ""+ Mathf.RoundToInt (timeLeft);
		}
	}
        public void StartGame () {
        catchSound.instance.StartBtn();
        startButton.SetActive (false);
        Score.SetActive(true);
        Timer.SetActive(true);
        TrashcanController.ToggleControl (true);
		StartCoroutine (Spawn ());
	}

	public IEnumerator Spawn () {
		yield return new WaitForSeconds(1.0f);
		counting = true;
		while (timeLeft > 0 ) {
			GameObject ball = balls [Random.Range (0, balls.Length)];
			Vector3 spawnPosition = new Vector3 (
				transform.position.x + Random.Range(-maxWidth, maxWidth), 
				transform.position.y, 
				0.0f
			);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (ball, spawnPosition, spawnRotation);
			yield return new WaitForSeconds (Random.Range (0.2f, 1.0f));
		}

		yield return new WaitForSeconds (2.0f);
        TrashcanController.ToggleControl(false);
        SceneManager.LoadScene("FailScene");
    }
}
