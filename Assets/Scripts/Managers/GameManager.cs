using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager: MonoBehaviour {

	public bool gamePaused = false;

	GameObject IMenu; 
	GameObject GameOverMenu; 

	void Start(){
		IMenu  = GameObject.Find ("UI/Canvas/IMenu");
		GameOverMenu = GameObject.Find ("UI/Canvas/GameOverMenu");
		UnpauseGame ();
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

	public void GameOver(){
		PauseGame ();
		GameOverMenu.SetActive(true);
	}

	public void RestartGame(){
		SceneManager.LoadScene ("Main");
	}

}
