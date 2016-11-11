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
			logo.transform.rotation = Quaternion.Lerp (logo.transform.rotation, Quaternion.Euler (Vector3.forward * 10f), 1);

			if (timeToChangeScene < timeWaited ){
				SceneManager.LoadScene("MMenu");
			}
		}

		timeWaited += Time.deltaTime;
	}

}
