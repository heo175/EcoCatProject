using UnityEngine;
using System.Collections;

public class Catch_StartGame : MonoBehaviour {

	public Catch_GameController gameController;

	void OnMouseDown () {
		gameController.StartGame ();
	}
}