using UnityEngine;
using System.Collections;

public class Missile : Projectile {

	void Start(){
		ExpireIn = 20; 
	}

	protected override void Move(Vector3 direction, float distThisFrame){
		base.Move (direction, distThisFrame);
		//transform.rotation = Quaternion.LookRotation (target.position);
	}

}

