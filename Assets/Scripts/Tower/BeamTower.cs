using UnityEngine;
using System.Collections;

public class BeamTower: Tower {

	public GameObject wave; 


	public override void Start(){
		base.Start ();
		towerType = (int)Tower.TowerType.Sniper; 
		range = 10f;
		fireCooldown = 10f;
		turret = transform;
		spawn_1 = turret.transform.Find ("spawn_point"); 
	}

	public override void Shoot(){
		GameObject s = (GameObject) Instantiate(projectilePrefab, spawn_1.transform.position, spawningRotation);
		GameObject wav = (GameObject) Instantiate(wave, spawn_1.transform.position, spawningRotation);
		s.transform.LookAt (nearestEnemy.transform.position);
		wav.transform.LookAt (nearestEnemy.transform.position);
		wav.transform.localScale *= 0.25f;
//		wav.transform.Rotate(Vector3.left, 90.0f);
		wav.GetComponent<BeamWave>().col = Color.blue;
		Destroy (s, .5f); 
		Destroy (wav, .5f); 
	}


	protected override void PlayShot(){
		if (shot != null) {
			audioSource.volume = .1f;
			audioSource.PlayOneShot (shot);
		}
	}
}
