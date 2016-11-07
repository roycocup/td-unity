using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class Splash : MonoBehaviour {

	void Update(){
		StartCoroutine (W ());
	}

	IEnumerator W(){
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene("MMenu");
	}
}
