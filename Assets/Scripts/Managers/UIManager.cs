using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

	Text healthText; 
	Text infoText; 
	Text moneyText;

	void Start () {
		healthText = GameObject.Find ("UI/Canvas/InGamePanel/HealthUIText").GetComponent<Text>(); 
		infoText = GameObject.Find ("UI/Canvas/InGamePanel/InfoUIText").GetComponent<Text>(); 
		moneyText = GameObject.Find ("UI/Canvas/InGamePanel/MoneyUIText").GetComponent<Text>(); 
		// it should be off, but just in case
		GameObject.Find ("UI/Canvas/TowerMenuUI").SetActive(false);
	}
		

	public void MainUIUpdate( Dictionary<string, string> info){
		healthText.text = "Health: " + info["health"];
		moneyText.text = "$ " + info["money"];
		infoText.text = "Next wave in " + info["displayTime"] + " seconds";
	}


	public void DisplayTowerMenu(){
		GameObject towerMenuUI  = GameObject.Find ("UI/Canvas/TowerMenuUI");
		towerMenuUI.SetActive (true); 
	}

	public void HideTowerMenu(){
		GameObject towerMenuUI  = GameObject.Find ("UI/Canvas/TowerMenuUI");
		towerMenuUI.SetActive (false); 
	}
}
