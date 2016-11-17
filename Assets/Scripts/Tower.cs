using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public const int TYPE_SNIPER = 0;  
	public const int TYPE_MISSILE = 1; 

	// public
	public int towerType = 0;
	public float range = 50f;
	public float rotationSpeed = 90f;
	public float fireCooldown = 2f;
	public int cost = 5;
	public GameObject projectilePrefab; 
	public GameObject smoke; 
	public AudioClip shot; 


	// private 
	protected Enemy nearestEnemy;
	protected float fireCooldownLeft = 0;
	protected Transform turret;
	protected Transform spawn;
	protected Quaternion spawningRotation;
	protected AudioSource audioSource;



	/**
	 * Methods
	 */

	public virtual void Start(){
		nearestEnemy = null; 
		audioSource = gameObject.GetComponent<AudioSource> (); 
		spawningRotation = Quaternion.identity;
	}


		
	void FixedUpdate(){

		float timeForNewTarget = 0; 
		if (timeForNewTarget <= 0) {
			FindNearestEnemy ();
			timeForNewTarget = 2f; 
		} else {
			timeForNewTarget -= Time.deltaTime; 
		}


		if (nearestEnemy != null && turret != null) {
			Vector3 dir = nearestEnemy.transform.position - turret.transform.position;
			Quaternion rotation = Quaternion.LookRotation (dir);

			// turret.transform.rotation = Quaternion.Lerp (turret.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
			// turret.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
			Quaternion newRotation = Quaternion.RotateTowards(turret.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
			// smoothing the rotation for the turret
			Vector3 e = newRotation.eulerAngles;
			e = new Vector3 (0, e.y, 0);
			turret.transform.rotation = Quaternion.Euler (e);

			//only shoot when its pointing at target
			// FIXME: Turret can't fire when its still seeking and the target is too close.
			if (Mathf.RoundToInt(turret.transform.rotation.eulerAngles.y) == Mathf.RoundToInt(rotation.eulerAngles.y) && nearestEnemy.Status != Statuses.DYING) {
				Shoot ();
			}
		}
	}

	public virtual void Shoot(){
		if (fireCooldownLeft <= 0) {
			// reset firecooldown
			fireCooldownLeft = fireCooldown; 

			if (spawn != null) {
				// instantiate the associated projectile prefab 
				GameObject projectile = (GameObject) Instantiate (projectilePrefab, spawn.transform.position, spawningRotation);

				// Assign a target to the projectile
				// We grab the base class of the script as we dont know the name of the specific script associated 
				projectile.GetComponent<Projectile> ().target = nearestEnemy.transform;
				PlayShot ();
			} 

		} else {
			// remove frametime from cooldowntime
			fireCooldownLeft -= Time.deltaTime; 	
		}
	}


	void FindNearestEnemy(){
		Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>(); 

		float distance = Mathf.Infinity; 

		foreach (Enemy e in enemies) {
			float d = Vector3.Distance (e.transform.position, transform.position);

			if (d <= range) {
				// if there is no other enemy OR the distance of this one is smaller than the previous one
				if (nearestEnemy == null || d < distance) {
					distance = d;
					nearestEnemy = e;
				}
			}
		}

		if (nearestEnemy == null) {
			//Debug.Log ("No enemies");
			return; 
		}
	}



	protected virtual void PlayShot(){}

}
