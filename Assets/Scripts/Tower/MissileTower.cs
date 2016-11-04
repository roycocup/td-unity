using UnityEngine;
using System.Collections;

public class MissileTower : Tower {

	override public void Start(){
		turret = transform;
		spawn = turret.Find ("SpawnLeft");
	}


}

