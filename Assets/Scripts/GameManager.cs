using UnityEngine;
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

		UIUpdate ();
		// dont start another wave if the spawn is not over
		if (spawning == false) {
			if (waveTimeLeft <= 0) {
				numElementsInWave = Random.Range (10, 20); 
				numElementsInWaveLeft = numElementsInWave;
				StartCoroutine (SpawnWave ());
				waveTimeLeft = waveRate;
			} else {
				waveTimeLeft -= Time.deltaTime; 
			}
		}

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
		for (int i = 0; i < Mathf.RoundToInt(Random.Range(1,2)); i++) {
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

		if (go != null)
			GameObject.Instantiate (go, enemySpawn.position, enemySpawn.rotation);			
	}


	void UIUpdate(){
		//health
		healthText.text = "Health: " + health.ToString ();

		//money
		moneyText.text = "$ " + money.ToString ();

		// info
		float displayTime = waveTimeLeft; 
		if (spawning == true) {
			displayTime = waveTimeLeft + (numElementsInWaveLeft * spawnRate); 
		} 
		infoText.text = "Next wave in " + displayTime.ToString ("F2") + " seconds";
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

	public void BuyTower (GameObject spotTrans, GameObject tower, int towerCost){
		if (SubMoney (towerCost) == true) {
			Instantiate (tower, spotTrans.transform.position, spotTrans.transform.rotation);
			Destroy (spotTrans);
		}
	}

}
