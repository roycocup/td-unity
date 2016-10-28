﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

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

	//objects
	public GameObject normalEnemyPrefab; 
	public GameObject eliteEnemyPrefab; 
	public GameObject sniperTowerPrefab; 
	Transform enemySpawn; 
	Text healthText; 
	Text infoText; 
	Text moneyText; 

	void Start(){
		enemySpawn = GameObject.Find ("EnemySpawn").transform;
		healthText = GameObject.Find ("UI/Canvas/Panel/HealthUIText").GetComponent<Text>(); 
		infoText = GameObject.Find ("UI/Canvas/Panel/InfoUIText").GetComponent<Text>(); 
		moneyText = GameObject.Find ("UI/Canvas/Panel/MoneyUIText").GetComponent<Text>(); 
	}

	void FixedUpdate(){

		HealthUpdate ();
		InfoUpdate ();
		MoneyUpdate ();
		// dont start another wave if the spawn is not over
		if (spawning == false) {
			if (waveTimeLeft <= 0) {
				numElementsInWave = Random.Range (10, 30); 
				numElementsInWaveLeft = numElementsInWave;
				StartCoroutine (SpawnWave ());
				waveTimeLeft = waveRate;
			} else {
				waveTimeLeft -= Time.deltaTime; 
			}
		}

	}

	void HealthUpdate(){
		healthText.text = "Health: " + health.ToString ();
	}

	void InfoUpdate(){
		float displayTime = waveTimeLeft; 
		if (spawning == true) {
			displayTime = waveTimeLeft + (numElementsInWaveLeft * spawnRate); 
		} 
		infoText.text = "Next wave in " + displayTime.ToString ("F2") + " seconds";
	}

	void MoneyUpdate(){
		moneyText.text = "$ " + money.ToString ();
	}


	IEnumerator SpawnWave(){
		while (numElementsInWaveLeft > 0) {
			spawning = true;
			if (spawnTimeLeft < 0) {
				SpawnEnemy ();
				numElementsInWaveLeft--; 
				spawnTimeLeft = spawnRate; 
				yield return null;
			} else {
				spawnTimeLeft -= Time.deltaTime;
				yield return null;
			}

		}
		numElementsInWaveLeft = numElementsInWave;
		spawning = false;
		yield return null;
	}

	void SpawnEnemy(){
		GameObject.Instantiate (normalEnemyPrefab, enemySpawn.position, enemySpawn.rotation);			
	}


	public void TakeDamage(int damage){
		health -= damage; 
		if (health < 1) {
			GameOver ();
		}
	}

	void GameOver(){
		//TODO: do something here to stop the game
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

	public void BuyTower (Transform spotTrans, GameObject tower, int towerCost){
		if (SubMoney (towerCost) == true) {
			Instantiate (tower, spotTrans.position, spotTrans.rotation);
		}
	}

}
