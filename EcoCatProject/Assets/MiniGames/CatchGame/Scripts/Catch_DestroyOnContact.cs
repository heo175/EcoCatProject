using UnityEngine;
using System.Collections;

public class Catch_DestroyOnContact : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D other) {
		Destroy (other.gameObject);
	}
}
