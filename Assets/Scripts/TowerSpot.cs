using UnityEngine;
using System.Collections;

public class TowerSpot : MonoBehaviour {

	public GameObject sniperTower; 
	UpgradeManager _manager; 

	void Start(){
		_manager = GameObject.Find("GameManager").GetComponent<UpgradeManager>();
	}

	void OnMouseUp(){
		_manager.DisplayTowerMenu(gameObject);
	}
}
