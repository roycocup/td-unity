using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System; 

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
	public GameObject missileTowerPrefab; 
	public GameObject towerSpotConstructionSmoke; 

	//private
	Transform enemySpawn; 
	Text healthText; 
	Text infoText; 
	Text moneyText;
	int numWaves = 0;

	// persisting the towerSpot for the new tower to be bought
	GameObject towerSpot; 

	void Start(){
		enemySpawn = GameObject.Find ("EnemySpawn").transform;
		healthText = GameObject.Find ("UI/Canvas/Panel/HealthUIText").GetComponent<Text>(); 
		infoText = GameObject.Find ("UI/Canvas/Panel/InfoUIText").GetComponent<Text>(); 
		moneyText = GameObject.Find ("UI/Canvas/Panel/MoneyUIText").GetComponent<Text>(); 
		// it should be off, but just in case
		GameObject.Find ("UI/Canvas/TowerMenuUI").SetActive(false);
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
			GameObject enemy = (GameObject) GameObject.Instantiate (go, enemySpawn.position, enemySpawn.rotation);
			int enemyHealth = enemy.GetComponent<Enemy> ().health;
			// adding strength to the wave
			enemy.GetComponent<Enemy> ().health = enemyHealth + (numWaves * 2);
		}
			
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


	// FIXME: Need to have 2 different prefabs which the scripts are extentions from Tower. 
	// There should be a factory for each one
	public void BuyTower (int towerType){
		GameObject tower = sniperTowerPrefab;
		if (towerType == Tower.TYPE_SNIPER) {
			tower = sniperTowerPrefab; 
		} else if (towerType == Tower.TYPE_MISSILE){
			tower = missileTowerPrefab; 
		}

		Tower towerScript = tower.GetComponent<Tower> (); 
		if (SubMoney (towerScript.cost) == true) {
			Instantiate (tower, towerSpot.transform.position, towerSpot.transform.rotation);
			towerScript.towerType = towerType;
			PlayConstructTower ();
			GameObject animation = (GameObject) Instantiate (towerSpotConstructionSmoke, towerSpot.transform.position, Quaternion.Euler(new Vector3(-90f, 0, 0)));
			Destroy (towerSpot);
			Destroy (animation, 5);
		}

		HideTowerMenu (); 
	}

	public void DisplayTowerMenu(GameObject spot){
		GameObject towerMenuUI  = GameObject.Find ("UI/Canvas/TowerMenuUI");
		towerMenuUI.SetActive (true); 
		towerSpot = spot; 
	}

	void HideTowerMenu(){
		GameObject towerMenuUI  = GameObject.Find ("UI/Canvas/TowerMenuUI");
		towerMenuUI.SetActive (false); 
	}


	void PlayConstructTower (){
		AudioSource constructionSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().GetAudio("construction-01");
		Debug.Log (constructionSound); 
		if (constructionSound != null && !constructionSound.isPlaying)
			constructionSound.Play ();
	}


}
