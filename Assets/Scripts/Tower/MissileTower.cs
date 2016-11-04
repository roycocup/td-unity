using UnityEngine;
using System.Collections;

public class MissileTower : Tower {



	override public void Start(){
		turret = transform;
		spawn = turret.Find ("SpawnLeft");
	}

	override public void Shoot(){
		if (nearestEnemy != null) spawningRotation = Quaternion.LookRotation (nearestEnemy.transform.position);
		base.Shoot();
	}


}

