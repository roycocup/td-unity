using UnityEngine;
using System.Collections;

public class Bullet : Projectile
{
	public GameObject particlesDie01;

	void Start(){
		damage = 1;
	}

	override protected void Die(){
		GameObject p = (GameObject) GameObject.Instantiate (particlesDie01, transform.position, Quaternion.identity);
		Destroy(gameObject);
		Destroy (p, 1); 
	}
	
}

