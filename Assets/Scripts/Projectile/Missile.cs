using UnityEngine;
using System.Collections;

public class Missile : Projectile {

	void Start(){
		radius = 3f;
		damage = 5;
		speed = 4f;
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
			Debug.DrawRay (transform.position, Vector3.forward, Color.blue);
		} else {
			base.Move (direction, distThisFrame);
		}
	}

}

