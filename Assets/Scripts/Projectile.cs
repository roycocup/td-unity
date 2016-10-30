﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed = 15f; 

	[System.NonSerialized]
	public Transform target = null; 

	float radius = 0.5f;
	int damage = 1;
	Vector3 direction; 

	// Update is called once per frame
	void FixedUpdate () {
		float distThisFrame = speed * Time.deltaTime;

		if (target != null) {
			direction = target.transform.position - transform.position; 
			float distance = direction.magnitude; 

			// if the distance left is less than the amount the bullet is to travel this frame
			if (distance < distThisFrame) {
				// reached the target
				DoBulletHit ();
			} else {
				transform.Translate (direction.normalized * distThisFrame);	
			}

			Destroy (gameObject, 2);
		} else {
			// if the target became null but we are already moving
			transform.Translate (direction.normalized * distThisFrame);
			Destroy(gameObject, 2);
		} 
	}


	void DoBulletHit() {
		Collider[] cols = Physics.OverlapSphere(transform.position, radius);

		foreach(Collider c in cols) {
			Enemy e = c.GetComponent<Enemy>();
			if(e != null) {
				// TODO: You COULD do a falloff of damage based on distance, but that's rare for TD games
				e.GetComponent<Enemy>().TakeDamage(damage);
			}
		}
		// TODO: Maybe spawn a cool "explosion" object here?
		Destroy(gameObject);
	}


}