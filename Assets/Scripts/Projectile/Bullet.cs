using UnityEngine;
using System.Collections;

public class Bullet : Projectile
{
	public GameObject explosionParticles;

	protected new void Start(){
		audioSource = gameObject.GetComponent<AudioSource> ();
		damage = 1;
	}

	protected override void Die(){
		GameObject explosion = (GameObject) GameObject.Instantiate (explosionParticles, transform.position, Quaternion.identity);
		// PlayExplosion ();
		Destroy(gameObject);
		Destroy (explosion, 1); 
	}
	
}

