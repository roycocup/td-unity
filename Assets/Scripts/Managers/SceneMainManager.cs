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
	Transform _enemySpawn01; 
	Transform _enemySpawn02; 
	int numWaves = 0;

	void Start(){
		_enemySpawn01 = GameObject.Find ("EnemySpawn01").transform;
		_enemySpawn02 = GameObject.Find ("EnemySpawn02").transform;
		_uiManager = gameObject.GetComponent<UIManager> ();
	}

	void FixedUpdate(){
		
		UIUpdate ();
		// dont start another wave if the spawn is not over
		if (_spawning == false) {
			if (_waveTimeLeft <= 0) {
				_numElementsInWave = UnityEngine.Random.Range (20, 40); 
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
		info.Add ("waveCount", numWaves.ToString ());
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

			// just randomizing the spoinpoints
			Transform spawnPoint = _enemySpawn01; 
			go.GetComponent<Enemy>().pathNumber = 1;
			if (randomZ > 0) {
				spawnPoint = _enemySpawn02; 
				go.GetComponent<Enemy>().pathNumber = 2;
			} 
				
			Vector3 rdmSpawnPoint = new Vector3(spawnPoint.position.x, spawnPoint.position.y,  spawnPoint.position.z + randomZ); 
			GameObject enemy = (GameObject) GameObject.Instantiate (go, rdmSpawnPoint, spawnPoint.rotation);
			int enemyHealth = enemy.GetComponent<Enemy> ().health;
			// adding strength to the wave
			enemy.GetComponent<Enemy> ().health = enemyHealth + (numWaves * 4);
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
		

}
