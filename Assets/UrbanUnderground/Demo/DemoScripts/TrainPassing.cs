using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPassing : MonoBehaviour {

	GameObject train;
	public AudioSource mainAudio;
	float audioVolume;

	public GameObject passingTrainPrefab;

	void Start() {
		audioVolume = mainAudio.volume;
	}

	void OnTriggerStay(Collider o) {
		if (o.tag.Equals ("Player") && train == null) {
			mainAudio.volume = 0.03f;
			if (passingTrainPrefab != null) {
				train = Instantiate (passingTrainPrefab) as GameObject;
			}
		}
	}

	IEnumerator OnTriggerExit(Collider c) {
		if (c.tag.Equals ("Player")) {
			mainAudio.volume = audioVolume;
			yield return new WaitForSeconds (0f);
			Destroy (train.gameObject);
		}
	}
}
