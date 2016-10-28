using UnityEngine;
using System.Collections;

public class TowerSpot : MonoBehaviour {


	void OnMouseUp(){
		GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
		Instantiate (gameManager.sniperTowerPrefab, transform.position, transform.rotation);
	}
}
