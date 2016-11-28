using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Statuses {SPAWNED, ALIVE, TAKINGFIRE, DYING};

public class Enemy : MonoBehaviour {

	//public
	public float speed = 2f; 
	public int health = 10;
	public const int TYPE_NORMAL = 0; 
	public const int TYPE_ELITE = 1; 
	public Animator animator; 
	public Image healthUI;

	//private
	protected float _maxHealth;
	protected Statuses _status;
	protected GameObject _path; 
	protected Transform _pathNode;
	protected float _rotation_speed; 
	protected int _nodeIndex = 0; 
	protected int _value = 1; // money value of this enemy
	protected float _randomZ, _randomX;
	protected SceneMainManager _sceneMainManager; 

	public Statuses Status {
		get {
			return _status;
		}
		set {
			_status = value;
		}
	}

	void Start (){
		_maxHealth = health;
		animator = gameObject.GetComponent<Animator> ();
		animator.SetInteger ("Health", health);
		_path = GameObject.Find ("Path");
		_pathNode = _path.transform.GetChild (_nodeIndex); 
		_rotation_speed = speed * 2f;
		_sceneMainManager = GameObject.Find ("GameManager").GetComponent<SceneMainManager>();
		_status = Statuses.SPAWNED;
		GetDestinationRandomization ();
	}

	void GetNextNode(){
		// if we have reached the last node, set it to null, and we assume we reached goal.
		if (_nodeIndex > (_path.transform.childCount - 1)) {
			_pathNode = null;
		} else {
			_pathNode = _path.transform.GetChild (_nodeIndex);
			_nodeIndex++;	
		}
	}

	void FixedUpdate(){
		if (_status != Statuses.DYING) {
			Move ();
		}
	}

	public void Move(){
		//FIXME: Enemies are goitn through the tower spots. The new randomization needs to take the direction line into account and recalculate if this happens.
		//base the direction roughly on the path
		Vector3 directionPoint = new Vector3 (_pathNode.transform.position.x + _randomX, _pathNode.transform.position.y, _pathNode.transform.position.z + _randomZ);
		// get direction to the node and go to it
		Vector3 direction = directionPoint - this.transform.position; 
		// get the distance the object is going to go this frame
		float distThisFrame = speed * Time.deltaTime;
		// maginiture will give us a simple float of the vector distance
		if (direction.magnitude <= distThisFrame) {
			GetNextNode ();
			if (_pathNode == null) {
				// no more nodes. We reached goal
				ReachedGoal();
			}
		} else {
			/*
			 *
			 * A normalized vector is really a vector for a direction and then we multiply it 
			 * by the amount we are supposed to move, we get a smooth translation
			 * 
			 * The Space.World parameter makes the translation relative to the world space rather than local
			 * because if this is local then the direction will change everytime I rotate the object. 
			 * So if I get the direction, move, rotate, get the new direction and now the direction is different. 
			 * So i keep moving towards a new direction al the time, unless the speed as which I move can catch up
			 * with the rotation speed. This will essentially create an orbit around a target, instead of turning and going towards it.
			 * Also remember that Translate uses Space.Self (Local) by default.
			 * 
			 * Also, I'm using a translate because none of these objects have a collider or rigidbody. Otherwise I should be using 
			 * physics forces.
			*/ 
			this.transform.Translate (direction.normalized * distThisFrame, Space.World);

			// Quaternions are a rotation position, not an amount to rotate.
			// Get the rotation position of looking for the target. 
			// apply a linear interpolation (ease in and out) between where you are facing and the new rotation position 
			// and the speed at which we should rotate.
			Quaternion rotation = Quaternion.LookRotation (direction); 
			this.transform.rotation = Quaternion.Lerp (transform.rotation, rotation, _rotation_speed * Time.deltaTime);
			// or this.transform.lookAt(pathNode);
			// or this.transform.rotation = Quaternion.LookRotation (direction); 
		}
	}

	void HealthUIDamage(int damage){
		float max = 0.1f;
		float z = healthUI.rectTransform.localScale.z;
		float y = healthUI.rectTransform.localScale.y;
		float x = Mathf.Clamp(((float) (health - damage ) * max) / _maxHealth, 0, max); 

		healthUI.rectTransform.localScale = new Vector3 (x, y, z);
	}

	public void TakeDamage(int damage){
		health -= damage;
		HealthUIDamage (damage);
		_status = Statuses.TAKINGFIRE;
		animator.SetInteger ("Health", health);
		if (health <= 0) {
			Die();
		}
	}
		

	protected void ReachedGoal(){
		_sceneMainManager.TakeDamage (1);
		Destroy(gameObject);
	}

	public void Die() {
		//GameObject.FindObjectOfType<ScoreManager>().money += moneyValue;
		_status = Statuses.DYING;
		_sceneMainManager.AddMoney (this._value);
		float dyingAnimationTime = 3.1f;
		Destroy(gameObject, dyingAnimationTime);
	}

	protected void GetDestinationRandomization(){
		_randomZ = UnityEngine.Random.Range(-3, 3); 
		_randomX = UnityEngine.Random.Range(-3, 3);
	}

}
