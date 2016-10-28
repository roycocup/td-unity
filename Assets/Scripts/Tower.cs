using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	const int TYPE_SNIPER = 0;  
	const int TYPE_MISSILE = 1;  

	// public
	public int towerType = 0;
	public float range = 50f; 
	public float rotationSpeed = 5f;
	public float fireCooldown = 2f;
	public GameObject bulletPrefab; 
	public int cost = 5; 

	// private 
	Enemy nearestEnemy;
	float fireCooldownLeft = 0;
	Transform turret;
	Transform spawn;
	AudioSource shootSound; 

	void Start(){
		nearestEnemy = null; 
		turret = transform.Find ("Turret");
		spawn = turret.transform.Find ("Barrel/Spawn_Point"); 
		shootSound = gameObject.GetComponent<AudioSource> (); 
	}
		
	void FixedUpdate(){
		
		FindNearestEnemy ();

		if (nearestEnemy != null && turret != null) {
			Vector3 dir = nearestEnemy.transform.position - turret.transform.position;
			Quaternion rotation = Quaternion.LookRotation (dir);

			// turret.transform.rotation = Quaternion.Lerp (turret.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
			// turret.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
			Quaternion newRotation = Quaternion.RotateTowards(turret.transform.rotation, rotation, 90f * Time.deltaTime);
			// smoothing the rotation for the turret
			Vector3 e = newRotation.eulerAngles;
			e = new Vector3 (0, e.y, 0);
			turret.transform.rotation = Quaternion.Euler (e);

			//only shoot when its pointing at target
			if (Mathf.RoundToInt(turret.transform.rotation.eulerAngles.y) == Mathf.RoundToInt(rotation.eulerAngles.y)) {
				Shoot ();
			}
		}
	}

	void Shoot(){
		if (fireCooldownLeft <= 0) {
			// reset firecooldown
			fireCooldownLeft = fireCooldown; 

			if (spawn != null) {
				GameObject Bullet = (GameObject) Instantiate (bulletPrefab, spawn.transform.position, Quaternion.identity);
				Bullet bulletScript = Bullet.GetComponent<Bullet> ();
				bulletScript.target = nearestEnemy.transform; 
				//PlayShoot ();
			} 

		} else {
			// remove frametime from cooldowntime
			fireCooldownLeft -= Time.deltaTime; 	
		}
	}


	void FindNearestEnemy(){
		Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>(); 

		float distance = Mathf.Infinity; 

		foreach (Enemy e in enemies) {
			float d = Vector3.Distance (e.transform.position, transform.position);

			if (d <= range) {
				// if there is no other enemy OR the distance of this one is smaller than the previous one
				if (nearestEnemy == null || d < distance) {
					distance = d;
					nearestEnemy = e;
				}
			}
		}

		if (nearestEnemy == null) {
			//Debug.Log ("No enemies");
			return; 
		}
	}



	void PlayShoot(){
		if (!shootSound.isPlaying)
			shootSound.Play ();
	}

}
