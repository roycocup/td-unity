using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	//public
	public float speed = 2f; 
	public int health = 10;
	public const int TYPE_NORMAL = 0; 
	public const int TYPE_ELITE = 1; 
	public Animator animator; 


	//private
	protected enum Status {SPAWNED, ALIVE, TAKINGFIRE, DYING};
	protected Status _status;
	protected GameObject Path; 
	protected Transform pathNode;
	protected float rotation_speed; 
	protected int nodeIndex = 0; 
	protected int value = 1; // money value of this enemy

	GameManager gameManager; 

	void Start (){
		animator = gameObject.GetComponent<Animator> ();
		Path = GameObject.Find ("Path");
		pathNode = Path.transform.GetChild (nodeIndex); 
		rotation_speed = speed * 2f;
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		_status = Status.SPAWNED;
	}

	void GetNextNode(){
		// if we have reached the last node, set it to null, and we assume we reached goal.
		if (nodeIndex > (Path.transform.childCount - 1)) {
			pathNode = null;
		} else {
			pathNode = Path.transform.GetChild (nodeIndex);
			nodeIndex++;	
		}
	}

	void FixedUpdate(){
		if (_status != Status.DYING) {
			Move ();
		}
	}

	public void Move(){
		// get direction to the node and go to it
		Vector3 direction = pathNode.transform.position - this.transform.position; 
		// get the distance the object is going to go this frame
		float distThisFrame = speed * Time.deltaTime;
		// maginiture will give us a simple float of the vector distance
		if (direction.magnitude <= distThisFrame) {
			GetNextNode ();
			if (pathNode == null) {
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
			this.transform.rotation = Quaternion.Lerp (transform.rotation, rotation, rotation_speed * Time.deltaTime);
			// or this.transform.lookAt(pathNode);
			// or this.transform.rotation = Quaternion.LookRotation (direction); 
		}
	}

	public void TakeDamage(int damage){
		health -= damage; 
		_status = Status.TAKINGFIRE;
		if (health <= 0) {
			Die();
		}
	}
		

	protected void ReachedGoal(){
		gameManager.TakeDamage (1);
		Destroy(gameObject);
	}

	public void Die() {
		//GameObject.FindObjectOfType<ScoreManager>().money += moneyValue;
		_status = Status.DYING;
		gameManager.AddMoney (this.value);
		animator.Play ("Dying", -1);
		Destroy(gameObject, 4);
	}

}
