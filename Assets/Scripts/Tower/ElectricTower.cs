using UnityEngine;
using System.Collections;

public class ElectricTower : Tower {

	protected Transform spawn_2;
	protected Transform spawn_3;
	protected byte lastShotFrom = 1;

	public override void Start(){
		base.Start ();
		towerType = (int)Tower.TowerType.Sniper; 
		range = 10f;
		fireCooldown = 10f;
		turret = transform;
		spawn_1 = turret.transform.Find ("Spawn01"); 
		spawn_2 = turret.transform.Find ("Spawn02"); 
	}

	public override void Shoot(){

		ShootingSystem();
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

		GameObject projectile = (GameObject) Instantiate (projectilePrefab, pipe, spawningRotation);
		//projectile.GetComponent<Projectile> ().target = nearestEnemy.transform;
		projectile.transform.LookAt(nearestEnemy.transform.position);
		Destroy (projectile, 1f);
		//PlayShot ();

	}


	protected override void PlayShot(){
		if (shot != null) {
			audioSource.volume = .1f;
			audioSource.PlayOneShot (shot);
		}
	}
}
