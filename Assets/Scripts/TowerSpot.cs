using UnityEngine;
using System.Collections;

public class TowerSpot : MonoBehaviour {

	public GameObject sniperTower; 
	SceneMainManager _smm; 

	void Start(){
		//UI.GetComponent<Canvas> ().enabled = false;
		_smm = GameObject.Find("GameManager").GetComponent<SceneMainManager>();
	}

	void OnMouseUp(){
		_smm.DisplayTowerMenu(gameObject);
		//gm.BuyTower (gameObject, sniperTower, sniperTower.GetComponent<Tower>().cost); 
	}
}
