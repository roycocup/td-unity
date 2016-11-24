using UnityEngine;
using System.Collections;

public class Bullet : Projectile
{
	public GameObject explosionParticles;

	void Start(){
		shootSound = gameObject.GetComponent<AudioSource> ();
		damage = 1;
	}

	protected override void Die(){
		GameObject explosion = (GameObject) GameObject.Instantiate (explosionParticles, transform.position, Quaternion.identity);
		// PlayDead ();
		Destroy(gameObject);
		Destroy (explosion, 1); 
	}
	
}

