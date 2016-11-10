using UnityEngine;
using System.Collections;

public class Bullet : Projectile
{
	public GameObject particlesDie01;

	void Start(){
		shootSound = gameObject.GetComponent<AudioSource> ();
		damage = 1;
	}

	protected override void Die(){
		GameObject explosion = (GameObject) GameObject.Instantiate (particlesDie01, transform.position, Quaternion.identity);
		// PlayDead ();
		Destroy(gameObject);
		Destroy (explosion, 1); 
	}
	
}

