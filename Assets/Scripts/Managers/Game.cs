using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Game : MonoBehaviour {

	public bool gamePaused = false;
	public GameObject IMenu; 

	void Start(){
		IMenu  = GameObject.Find ("UI/Canvas/IMenu");
	}

	void Update () {
		if (Input.GetButtonDown ("Cancel")) {
			if (gamePaused == true) {
				HideIMenu ();
			} else {
				ShowIMenu ();
			}
		}
	}


	public void ShowIMenu(){
		PauseGame ();
		IMenu.SetActive (true);
	}

	public void HideIMenu(){
		UnpauseGame ();
		IMenu.SetActive (false);
	}

	public void GoToMMenu(){
		SceneManager.LoadScene ("MMenu");
	}

	public void PauseGame(){
		Time.timeScale = 0;
		gamePaused = true;
	}

	public void UnpauseGame(){
		Time.timeScale = 1f;
		gamePaused = false;
	}

}
