using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DualTurret : Tower {

	protected Transform spawn_2;

	List<GameObject> bulletPool; 
	byte lastShotFrom = 2;

	public override void Start(){
		base.Start ();
		range = 10f;
		fireCooldown = 0.1f;
		turret = gameObject.transform.Find("Head");
		spawn_1 = turret.transform.Find ("Cannon_1/BulletSpawn_1"); 
		spawn_2 = turret.transform.Find ("Cannon_2/BulletSpawn_2"); 
		CreateBulletPool ();
	}


	void CreateBulletPool (){
		bulletPool = new List<GameObject> ();
		for (int i = 0; i <= 20; i++) {
			GameObject projectile = (GameObject)Instantiate (projectilePrefab, spawn_1.transform.position, spawningRotation);
			projectile.SetActive (false);
			bulletPool.Add(projectile);
		}	
	}


	protected override void ShootingSystem(){
		Vector3 pipe;
		if (lastShotFrom == 2) {
			pipe = spawn_1.transform.position;
			lastShotFrom = 1;
		} else {
			pipe = spawn_2.transform.position;
			lastShotFrom = 2;
		}
			
		GameObject projectile = (GameObject)Instantiate (projectilePrefab, pipe, spawningRotation);

		projectile.GetComponent<Projectile> ().target = nearestEnemy.transform;
		PlayShot ();

		if (smoke != null) {
			GameObject s = (GameObject) Instantiate (smoke, pipe, transform.rotation);
			Destroy (s, 1); 
		}
	}


	protected override void PlayShot(){
		if (shot != null) {
			audioSource.volume = .1f;
			audioSource.PlayOneShot (shot);
		}
	}

}

