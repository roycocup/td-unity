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


	// private 
	Enemy _nearestEnemy;
	public Enemy NearestEnemy {
		get {
			return _nearestEnemy;
		}
		set {
			_nearestEnemy = value;
		}
	}

	float _fireCooldownLeft = 0;
	public float FireCooldownLeft {
		get {
			return _fireCooldownLeft;
		}
		set {
			_fireCooldownLeft = value;
		}
	}

	Transform _turret;
	public Transform Turret {
		get {
			return _turret;
		}
		set {
			_turret = value;
		}
	}

	Transform _spawn;
	public Transform Spawn {
		get {
			return _spawn;
		}
		set {
			_spawn = value;
		}
	}

	AudioSource _shootSound;
	public AudioSource ShootSound {
		get {
			return _shootSound;
		}
		set {
			_shootSound = value;
		}
	}

 

	/**
	 * Methods
	 */


	public virtual void Start(){
		_nearestEnemy = null; 
		_turret = transform.Find ("Turret");
		_spawn = _turret.transform.Find ("Barrel/Spawn_Point"); 
		_shootSound = gameObject.GetComponent<AudioSource> (); 
	}


		
	void FixedUpdate(){

		float timeForNewTarget = 0; 
		if (timeForNewTarget <= 0) {
			FindNearestEnemy ();
			timeForNewTarget = 2f; 
		} else {
			timeForNewTarget -= Time.deltaTime; 
		}


		if (_nearestEnemy != null && _turret != null) {
			Vector3 dir = _nearestEnemy.transform.position - _turret.transform.position;
			Quaternion rotation = Quaternion.LookRotation (dir);

			// turret.transform.rotation = Quaternion.Lerp (turret.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
			// turret.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
			Quaternion newRotation = Quaternion.RotateTowards(_turret.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
			// smoothing the rotation for the turret
			Vector3 e = newRotation.eulerAngles;
			e = new Vector3 (0, e.y, 0);
			_turret.transform.rotation = Quaternion.Euler (e);

			//only shoot when its pointing at target
			// FIXME: Turret can't fire when its still seeking and the target is too close.
			if (Mathf.RoundToInt(_turret.transform.rotation.eulerAngles.y) == Mathf.RoundToInt(rotation.eulerAngles.y)) {
				Shoot ();
			}
		}
	}

	virtual public void Shoot(){
		if (_fireCooldownLeft <= 0) {
			// reset firecooldown
			_fireCooldownLeft = fireCooldown; 

			if (_spawn != null) {
				// instantiate the associated projectile prefab 
				GameObject projectile = (GameObject) Instantiate (projectilePrefab, _spawn.transform.position, Quaternion.identity);

				// Assign a target to the projectile
				// We grab the base class of the script as we dont know the name of the specific script associated 
				projectile.GetComponent<Projectile> ().target = _nearestEnemy.transform;

				//PlayShoot ();
			} 

		} else {
			// remove frametime from cooldowntime
			_fireCooldownLeft -= Time.deltaTime; 	
		}
	}


	void FindNearestEnemy(){
		Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>(); 

		float distance = Mathf.Infinity; 

		foreach (Enemy e in enemies) {
			float d = Vector3.Distance (e.transform.position, transform.position);

			if (d <= range) {
				// if there is no other enemy OR the distance of this one is smaller than the previous one
				if (_nearestEnemy == null || d < distance) {
					distance = d;
					_nearestEnemy = e;
				}
			}
		}

		if (_nearestEnemy == null) {
			//Debug.Log ("No enemies");
			return; 
		}
	}



	void PlayShoot(){
		if (!_shootSound.isPlaying)
			_shootSound.Play ();
	}

}
