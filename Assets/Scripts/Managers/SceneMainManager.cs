using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SceneMainManager : MonoBehaviour {

	// time between elements in a wave
	public float spawnRate = 5f;
	float spawnTimeLeft = 0; 
	// time to wait between waves
	public float waveRate = 10f;
	float waveTimeLeft = 1f;
	int numElementsInWave = 0;
	int numElementsInWaveLeft = 0; 
	bool spawning = false;
	public int health = 10; 
	public int money = 10; 

	UIManager _uiManager; 

	//objects
	public GameObject normalEnemyPrefab; 
	public GameObject eliteEnemyPrefab; 
	public GameObject sniperTowerPrefab; 
	public GameObject missileTowerPrefab; 
	public GameObject dualTurretPrefab; 
	public GameObject towerSpotConstructionSmoke; 

	//private
	Transform enemySpawn; 
	int numWaves = 0;

	// persisting the towerSpot for the new tower to be bought
	GameObject towerSpot; 

	void Start(){
		enemySpawn = GameObject.Find ("EnemySpawn").transform;
		_uiManager = gameObject.GetComponent<UIManager> ();
	}

	void FixedUpdate(){
		
		UIUpdate ();
		// dont start another wave if the spawn is not over
		if (spawning == false) {
			if (waveTimeLeft <= 0) {
				numElementsInWave = UnityEngine.Random.Range (10, 20); 
				numElementsInWaveLeft = numElementsInWave;
				numWaves++;
				StartCoroutine (SpawnWave ());
				waveTimeLeft = waveRate;
			} else {
				waveTimeLeft -= Time.deltaTime; 
			}
		}

	}

	void UIUpdate(){
		float displayTime = waveTimeLeft; 
		if (spawning == true) {
			displayTime = waveTimeLeft + (numElementsInWaveLeft * spawnRate); 
		} 
		Dictionary<string,string> info = new Dictionary<string,string> ();
		info.Add("health", health.ToString());
		info.Add ("money", money.ToString ());
		info.Add("displayTime", displayTime.ToString("F2"));
		_uiManager.MainUIUpdate (info);
	}

	IEnumerator SpawnWave(){
		while (numElementsInWaveLeft > 0) {
			spawning = true;
			if (spawnTimeLeft < 0) {
				SpawnEnemy (Enemy.TYPE_NORMAL);
				numElementsInWaveLeft--; 
				spawnTimeLeft = spawnRate; 
				yield return null;
			} else {
				spawnTimeLeft -= Time.deltaTime;
				yield return null;
			}

		}
		// always add a random number of elite enemies at the end
		for (int i = 0; i < Mathf.RoundToInt(UnityEngine.Random.Range(1,3)); i++) {
			yield return new WaitForSeconds(spawnRate);
			SpawnEnemy (Enemy.TYPE_ELITE);
		}
		numElementsInWaveLeft = numElementsInWave;
		spawning = false;
		yield return null;
	}

	void SpawnEnemy(int type){
		GameObject go = null; 
		if (type == Enemy.TYPE_NORMAL) {
			go = normalEnemyPrefab; 
		} else if (type == Enemy.TYPE_ELITE){
			go = eliteEnemyPrefab; 
		} 
		 
		if (go != null) {
			float randomZ = UnityEngine.Random.Range (-5, 5);
			Vector3 rdmSpawnPoint = new Vector3(enemySpawn.position.x, enemySpawn.position.y,  enemySpawn.position.z + randomZ); 
			GameObject enemy = (GameObject) GameObject.Instantiate (go, rdmSpawnPoint, enemySpawn.rotation);
			int enemyHealth = enemy.GetComponent<Enemy> ().health;
			// adding strength to the wave
			enemy.GetComponent<Enemy> ().health = enemyHealth + (numWaves * 2);
		}
			
	}


	public void TakeDamage(int damage){
		health -= damage; 
		if (health < 1) {
			GameOver ();
		}
	}

	void GameOver(){
		gameObject.GetComponent<GameManager> ().GameOver ();
	}

	public void AddMoney(int money){
		this.money += money; 
	}

	public bool SubMoney(int money){
		if ((this.money - money) < 0) {
			return false;
		}
		this.money -= money; 
		return true;
	}


	// FIXME: Need to have 2 different prefabs which the scripts are extentions from Tower. 
	// There should be a factory for each one
	public void BuyTower (int towerType){
		GameObject tower = sniperTowerPrefab;

		switch (towerType) {
		case Tower.TYPE_SNIPER:
			tower = sniperTowerPrefab; 
			break;
		case Tower.TYPE_MISSILE:
			tower = missileTowerPrefab; 
			break;
		case Tower.TYPE_DUAL:
			tower = dualTurretPrefab; 
			break;
		}

		Tower towerScript = tower.GetComponent<Tower> (); 
		if (SubMoney (towerScript.cost) == true) {
			GameObject newTower = Instantiate (tower, towerSpot.transform.position, towerSpot.transform.rotation) as GameObject;
			newTower.transform.Translate (Vector3.zero); // This is so that the physics engine can update the existance of this tower and we get hovering on it.
			towerScript.towerType = towerType;
			PlayConstructTower ();
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


	void PlayConstructTower (){
//		AudioSource constructionSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().GetAudio("construction-01");
//		if (constructionSound != null && !constructionSound.isPlaying)
//			constructionSound.Play ();
	}
		

}
