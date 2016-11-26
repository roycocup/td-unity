using UnityEngine;
using System.Collections;

public class Missile : Projectile {

	void Start(){
		radius = 3f;
		damage = 5;
		speed = 10f;
		expireIn = 20;
	}

	protected override void Move(Vector3 direction, float distThisFrame){
		//base.Move (direction, distThisFrame);
		if (target != null) {
			direction = target.transform.position - transform.position;
			transform.Translate (direction.normalized * speed * Time.deltaTime, Space.World);

			// transform.rotation = Quaternion.LookRotation (target.position, Vector3.up);
			// transform.rotation = Quaternion.identity;
			// transform.rotation = Quaternion.Euler (direction);
			transform.LookAt(target.position);
		} else {
			// if you lose your target, just keep going forward
			// TODO: if it still crosses any enemy, explode and damage
			base.Move (Vector3.forward, distThisFrame);
		}
	}

}

