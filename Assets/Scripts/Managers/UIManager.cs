using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

	Text healthText; 
	Text infoText; 
	Text moneyText;
	Text waveCount; 

	void Start () {
		healthText = GameObject.Find ("UI/Canvas/InGamePanel/HealthUIText").GetComponent<Text>(); 
		infoText = GameObject.Find ("UI/Canvas/InGamePanel/InfoUIText").GetComponent<Text>(); 
		moneyText = GameObject.Find ("UI/Canvas/InGamePanel/MoneyUIText").GetComponent<Text>(); 
		waveCount = GameObject.Find ("UI/Canvas/InGamePanel/WaveCount").GetComponent<Text>(); 
		// it should be off, but just in case
		GameObject.Find ("UI/Canvas/TowerMenuUI").SetActive(false);
	}
		

	public void MainUIUpdate( Dictionary<string, string> info){
		healthText.text = "Health: " + info["health"];
		moneyText.text = "$ " + info["money"];
		infoText.text = "Next wave in " + info["displayTime"] + " seconds";
		waveCount.text = "Wave #" + info["waveCount"];
	}




	public void DisplayTowerMenu(){
		GameObject towerMenuUI  = GameObject.Find ("UI/Canvas/TowerMenuUI");
		towerMenuUI.SetActive (true); 
	}

	public void HideTowerMenu(){
		GameObject towerMenuUI  = GameObject.Find ("UI/Canvas/TowerMenuUI");
		towerMenuUI.SetActive (false); 
	}

	public void DisplayUpgradeTowerMenu(GameObject selectedTower){
		GameObject towerMenuUI  = GameObject.Find ("UI/Canvas/TowerUpgradeUI");
		towerMenuUI.SetActive (true); 
	}

	public void HideUpgradeTowerMenu(){
		GameObject towerMenuUI  = GameObject.Find ("UI/Canvas/TowerUpgradeUI");
		towerMenuUI.SetActive (false); 
	}

	public void HideInstructions(){
		GameObject menu  = GameObject.Find ("UI/Canvas/Instructions");
		menu.SetActive (false); 
	}
}
