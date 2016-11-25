using UnityEngine;
using System.Collections;

public class DualTurret : Tower {

	public override void Start(){
		base.Start ();
		range = 10f;
		fireCooldown = 0.1f;
		turret = gameObject.transform.Find("Head");
		spawn = turret.transform.Find ("BulletSpawn_1"); 
	}

	public override void Shoot(){
		base.Shoot ();
		if (smoke != null) {
			GameObject s = (GameObject) Instantiate (smoke, spawn.transform.position, transform.rotation);
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

