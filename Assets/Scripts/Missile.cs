using UnityEngine;
using System.Collections;

public class Missile : Projectile {

	void Start(){
		ExpireIn = 20; 
	}

	protected override void Move(Vector3 direction, float distThisFrame){
		//base.Move (direction, distThisFrame);
		if (target != null) {
			direction = target.transform.position - transform.position;
			transform.Translate (direction.normalized * speed * Time.deltaTime, Space.World);

			//transform.rotation = Quaternion.LookRotation (target.position, Vector3.up);
			transform.LookAt (target.position);
		} else {
			base.Move (direction, distThisFrame);
		}
	}

}

