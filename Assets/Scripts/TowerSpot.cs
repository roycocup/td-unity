using UnityEngine;
using System.Collections;

public class TowerSpot : MonoBehaviour {

	public GameObject sniperTower; 

	void OnMouseUp(){
		GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();

		gameManager.BuyTower (transform, sniperTower, sniperTower.GetComponent<Tower>().cost); 
	}
}
