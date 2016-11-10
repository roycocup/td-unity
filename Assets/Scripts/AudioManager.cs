using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour {

	public AudioSource GetAudio(string filename){
		List<AudioSource> audioSources = new List<AudioSource>(GetComponentsInChildren<AudioSource>());

		AudioSource aSource = audioSources.Find (audioSource => audioSource.name == filename);
		if (aSource != null) {
			return aSource;
		} else {
			return null;
		}

	}
}
