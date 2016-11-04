using UnityEngine;
using System.Collections;

public class SniperTower : Tower {

	virtual public void Start(){
		turret = transform;
		spawn = turret.transform.Find ("spawn_point"); 
	}


}
