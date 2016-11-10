using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class Splash : MonoBehaviour {

	float timeToWait = 1.5f;
	float timeWaited = 0;
	int timeToChangeScene = 0;
	GameObject logo; 


	void Start () {
		logo = GameObject.Find("/Canvas/Panel/Logo");
		timeToChangeScene = 2 + (int) timeToWait;
	}


	void Update () {

		// animation time
		if (timeToWait < timeWaited) {
			Quaternion rotation =  Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.forward * 90f), 1f);
			transform.rotation = rotation;
			if (timeToChangeScene < timeWaited ){
				SceneManager.LoadScene("MMenu");
			}
		}

		timeWaited += Time.deltaTime;

	}

}
