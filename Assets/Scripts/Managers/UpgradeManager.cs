using UnityEngine;
using System.Collections;

public class UpgradeManager : MonoBehaviour {


	public GameObject sniperTower01;
	public GameObject sniperTower02;
	public GameObject missileTower01;
	public GameObject towerSpotConstructionSmoke; 

	UIManager _uiManager;
	SceneMainManager _sceneManager; 
	GameObject towerSpot; // persisting the towerSpot for the new tower to be bought
	GameObject tower;
	int upgradableTowerType; 


	void Start(){
		_uiManager = gameObject.GetComponent<UIManager> ();
		_sceneManager = gameObject.GetComponent<SceneMainManager> ();
	}


	// FIXME: Need to have 2 different prefabs which the scripts are extentions from Tower. 
	// There should be a factory for each one
	public void BuyTower (int towerType){
		GameObject tower = sniperTower01;

		switch (towerType) {
		case (int) Tower.TowerType.Sniper:
			tower = sniperTower01; 
			break;
		case (int) Tower.TowerType.Missile:
			tower = missileTower01; 
			break;
		}

		Tower towerScript = tower.GetComponent<Tower> (); 
		if (_sceneManager.SubMoney (towerScript.cost) == true) {
			GameObject newTower = Instantiate (tower, towerSpot.transform.position, towerSpot.transform.rotation) as GameObject;
			newTower.transform.Translate (Vector3.zero); // This is so that the physics engine can update the existance of this tower and we get hovering on it.
			towerScript.towerType = towerType;
			GameObject animation = (GameObject)Instantiate (towerSpotConstructionSmoke, towerSpot.transform.position, Quaternion.Euler (new Vector3 (-90f, 0, 0)));
			Destroy (towerSpot);
			Destroy (animation, 5);
		} else {
			FlashMessages f = gameObject.GetComponent<FlashMessages> ();
			f.Message = "Not enough money!"; 
		}

		_uiManager.HideTowerMenu (); 
	}


	public void DisplayTowerMenu(GameObject spot){
		_uiManager.DisplayTowerMenu ();
		towerSpot = spot; 
	}

	void HideTowerMenu(){
		_uiManager.HideTowerMenu ();
	}

	public void DisplayUpgradeTowerMenu(GameObject tower){
		_uiManager.DisplayUpgradeTowerMenu (tower);
		this.tower = tower; 
		upgradableTowerType = tower.GetComponent<Tower> ().towerType;
	}

	void HideUpgradeTowerMenu(){
		_uiManager.HideUpgradeTowerMenu ();
	}


	public void UpgradeTower (){

		GameObject newTower = null; 

		switch (upgradableTowerType) {
		case (int) Tower.TowerType.Sniper:
			// what level is it on? 
			if(tower.GetComponent<Tower>().upgradeLevel == 0){
				newTower = sniperTower02;
			}
			break;
		case (int) Tower.TowerType.Missile:
			newTower = missileTower01; 
			break;
		}

		if (!newTower.Equals(null)) {
			Tower towerScript = newTower.GetComponent<Tower> (); 
			if (_sceneManager.SubMoney (towerScript.cost) == true) {
				GameObject newTowerInstance = Instantiate (newTower, tower.transform.position, tower.transform.rotation) as GameObject;
				newTowerInstance.transform.Translate (Vector3.zero); // This is so that the physics engine can update the existance of this tower and we get hovering on it.
				towerScript.towerType = this.upgradableTowerType;
				towerScript.upgradeLevel++;
				GameObject animation = (GameObject)Instantiate (towerSpotConstructionSmoke, tower.transform.position, Quaternion.Euler (new Vector3 (-90f, 0, 0)));
				Destroy (tower);
				Destroy (animation, 5);
			} else {
				FlashMessages f = gameObject.GetComponent<FlashMessages> ();
				f.Message = "Not enough money!"; 
			}
		}
		_uiManager.HideUpgradeTowerMenu (); 
	}


}
