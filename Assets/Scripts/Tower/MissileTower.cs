using UnityEngine;
using System.Collections;

public class MissileTower : Tower {



	override public void Start(){
		Turret = transform;
		Spawn = Turret.Find ("SpawnLeft"); 
	}


}

