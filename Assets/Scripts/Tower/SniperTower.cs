using UnityEngine;
using System.Collections;

public class SniperTower : Tower {

	public override void Start(){
		base.Start ();
		range = 10f;
		fireCooldown = 0.1f;
		turret = transform;
		spawn_1 = turret.transform.Find ("spawn_point"); 
	}

	public override void Shoot(){
		base.Shoot ();
		if (smoke != null) {
			GameObject s = (GameObject) Instantiate (smoke, spawn_1.transform.position, transform.rotation);
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
