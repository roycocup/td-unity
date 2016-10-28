using UnityEngine;
using System.Collections;

public class TowerSpot : MonoBehaviour {

	public GameObject sniperTower; 
//	public GameObject UI; 

	void Start(){
		//UI.GetComponent<Canvas> ().enabled = false;
	}

	void OnMouseUp(){
		GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
		//UI.GetComponent<Canvas> ().enabled = true;
		gameManager.BuyTower (gameObject, sniperTower, sniperTower.GetComponent<Tower>().cost); 
	}
}
