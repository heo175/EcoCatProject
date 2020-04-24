﻿using UnityEngine;
using System.Collections;

public class Catch_Explode : MonoBehaviour {
	public GameObject explosion;
	public ParticleSystem[] effects;

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Trashcan") {
			Instantiate (explosion, transform.position, transform.rotation);
			foreach (var effect in effects) {
				effect.transform.parent = null;
				effect.Stop ();
				Destroy (effect.gameObject, 1.0f);
			}
			Destroy (gameObject);
		}
	}
}
