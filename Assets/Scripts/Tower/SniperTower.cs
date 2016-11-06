using UnityEngine;
using System.Collections;

public class SniperTower : Tower {

	override public void Start(){
		range = 10f;
		fireCooldown = 0.1f;
		turret = transform;
		spawn = turret.transform.Find ("spawn_point"); 
	}

	override public void Shoot(){
		base.Shoot ();
		if (smoke != null) {
			GameObject s = (GameObject) Instantiate (smoke, spawn.transform.position, transform.rotation);
			Destroy (s, 1); 
		}

	}

}
