using UnityEngine;
using System.Collections;

public class UpgradeManager : MonoBehaviour {


	public GameObject sniperTowerPrefab;
	public GameObject missileTowerPrefab;
	public GameObject dualTurretPrefab;
	public GameObject towerSpotConstructionSmoke; 
	UIManager _uiManager;
	SceneMainManager _sceneManager; 
	GameObject towerSpot; // persisting the towerSpot for the new tower to be bought
	GameObject tower;


	void Start(){
		_uiManager = gameObject.GetComponent<UIManager> ();
		_sceneManager = gameObject.GetComponent<SceneMainManager> ();
	}


	// FIXME: Need to have 2 different prefabs which the scripts are extentions from Tower. 
	// There should be a factory for each one
	public void BuyTower (int towerType){
		GameObject tower = sniperTowerPrefab;

		switch (towerType) {
		case (int) Tower.TowerType.Sniper:
			tower = sniperTowerPrefab; 
			break;
		case (int) Tower.TowerType.Missile:
			tower = missileTowerPrefab; 
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

		HideTowerMenu (); 
	}


	public void DisplayTowerMenu(GameObject spot){
		_uiManager.DisplayTowerMenu ();
		towerSpot = spot; 
	}

	void HideTowerMenu(){
		_uiManager.HideTowerMenu ();
	}

	public void DisplayUpgradeTowerMenu(GameObject go){
		_uiManager.DisplayUpgradeTowerMenu (go);
		tower = go; 
	}

	void HideUpgradeTowerMenu(){
		_uiManager.HideTowerMenu ();
	}


}
