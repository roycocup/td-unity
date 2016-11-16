using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// Public 
	public float radius = 0.5f;
	public int damage = 1;
	public Transform target = null; 

	// Protected
	protected float speed = 15f; 
	protected int expireIn = 5;
	protected Vector3 direction; 
	protected AudioSource shootSound;


	// Methods

	void Start(){
		shootSound = gameObject.GetComponent<AudioSource> (); 
	}

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
				Move (direction, distThisFrame);
			}
			Destroy (gameObject, expireIn);
		} else {
			// if the target became null but we are already moving
			Move (direction, distThisFrame);
			Destroy(gameObject, expireIn);
		} 
	}


	protected virtual void DoBulletHit() {
		Collider[] cols = Physics.OverlapSphere(transform.position, radius);

		foreach(Collider c in cols) {
			Enemy e = c.GetComponent<Enemy>();
			if(e != null) {
				print ("Hit" + e.name);
				// TODO: You COULD do a falloff of damage based on distance, but that's rare for TD games
				e.GetComponent<Enemy>().TakeDamage(damage);
			}
		}
		Die ();
	}

	protected virtual void Die(){
		Destroy(gameObject);
	}

	protected virtual void PlayDead(){
		if (!shootSound.isPlaying)
			shootSound.Play ();
	}

	protected virtual void Move(Vector3 direction, float distThisFrame){
		transform.Translate (direction.normalized * distThisFrame);	
	}
}
