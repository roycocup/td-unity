using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class MMenu : MonoBehaviour {

	public void OnNewGamePress(){
		SceneManager.LoadScene ("Main");
	}
}
