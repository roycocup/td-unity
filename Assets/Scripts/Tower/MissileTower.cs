using UnityEngine;
using System.Collections;

public class MissileTower : Tower {



	override public void Start(){
		Turret = transform;
		Spawn = Turret.Find ("SpawnLeft"); 
	}


	override public void Shoot(){
		//ProjectileRotation = Quaternion.LookRotation (NearestEnemy.transform.position);  
		ProjectileRotation = Quaternion.Euler (15, 15, 16); 
		base.Shoot ();
	}

}

