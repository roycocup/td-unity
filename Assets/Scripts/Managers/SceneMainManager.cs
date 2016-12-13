using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SceneMainManager : MonoBehaviour {

	//objects
	public GameObject normalEnemyPrefab; 
	public GameObject eliteEnemyPrefab; 
	public GameObject sniperTowerPrefab; 
	public GameObject missileTowerPrefab; 
	public GameObject dualTurretPrefab; 
	public GameObject towerSpotConstructionSmoke; 
	public float spawnRate = 5f; // time between elements in a wave
	public float waveRate = 10f; // time to wait between waves
	public int health = 10; 
	public int money = 10; 


	//private
	float _spawnTimeLeft = 0; 
	float _waveTimeLeft = 1f;
	int _numElementsInWave = 0;
	int _numElementsInWaveLeft = 0; 
	bool _spawning = false;
	UIManager _uiManager; 
	Transform _enemySpawn; 
	int numWaves = 0;





	// persisting the towerSpot for the new tower to be bought
	GameObject towerSpot; 

	void Start(){
		_enemySpawn = GameObject.Find ("EnemySpawn").transform;
		_uiManager = gameObject.GetComponent<UIManager> ();
	}

	void FixedUpdate(){
		
		UIUpdate ();
		// dont start another wave if the spawn is not over
		if (_spawning == false) {
			if (_waveTimeLeft <= 0) {
				_numElementsInWave = UnityEngine.Random.Range (10, 20); 
				_numElementsInWaveLeft = _numElementsInWave;
				numWaves++;
				StartCoroutine (SpawnWave ());
				_waveTimeLeft = waveRate;
			} else {
				_waveTimeLeft -= Time.deltaTime; 
			}
		}

	}

	void UIUpdate(){
		float displayTime = _waveTimeLeft; 
		if (_spawning == true) {
			displayTime = _waveTimeLeft + (_numElementsInWaveLeft * spawnRate); 
		} 
		Dictionary<string,string> info = new Dictionary<string,string> ();
		info.Add("health", health.ToString());
		info.Add ("money", money.ToString ());
		info.Add("displayTime", displayTime.ToString("F2"));
		_uiManager.MainUIUpdate (info);
	}

	IEnumerator SpawnWave(){
		while (_numElementsInWaveLeft > 0) {
			_spawning = true;
			if (_spawnTimeLeft < 0) {
				SpawnEnemy (Enemy.TYPE_NORMAL);
				_numElementsInWaveLeft--; 
				_spawnTimeLeft = spawnRate; 
				yield return null;
			} else {
				_spawnTimeLeft -= Time.deltaTime;
				yield return null;
			}

		}
		// always add a random number of elite enemies at the end
		for (int i = 0; i < Mathf.RoundToInt(UnityEngine.Random.Range(1,3)); i++) {
			yield return new WaitForSeconds(spawnRate);
			SpawnEnemy (Enemy.TYPE_ELITE);
		}
		_numElementsInWaveLeft = _numElementsInWave;
		_spawning = false;
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
			Vector3 rdmSpawnPoint = new Vector3(_enemySpawn.position.x, _enemySpawn.position.y,  _enemySpawn.position.z + randomZ); 
			GameObject enemy = (GameObject) GameObject.Instantiate (go, rdmSpawnPoint, _enemySpawn.rotation);
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
