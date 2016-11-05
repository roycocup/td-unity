using UnityEngine;
using System.Collections;

public class SniperTower : Tower {

	override public void Start(){
		range = 10f;
		fireCooldown = 0.1f;
		turret = transform;
		spawn = turret.transform.Find ("spawn_point"); 
	}


}
