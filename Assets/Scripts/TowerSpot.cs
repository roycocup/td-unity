using UnityEngine;
using System.Collections;

public class TowerSpot : MonoBehaviour {

	public GameObject sniperTower; 
	GameManager gm; 

	void Start(){
		//UI.GetComponent<Canvas> ().enabled = false;
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void OnMouseUp(){
		gm.DisplayTowerMenu(gameObject);
		//gm.BuyTower (gameObject, sniperTower, sniperTower.GetComponent<Tower>().cost); 
	}
}
