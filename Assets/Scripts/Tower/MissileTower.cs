using UnityEngine;
using System.Collections;

public class MissileTower : Tower {

	protected Transform spawn_2;
	byte lastShotFrom = 2;

	override public void Start(){
		base.Start ();
		cost = 5; 
		turret = transform;
		spawn_1 = turret.Find ("SpawnLeft");
		spawn_2 = turret.Find ("SpawnRight"); 
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

