using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {

	Transform _camera; 
	Vector3 _mouseFirstPoint; 
//	int _layerMask = 9;
	float movingSpeed = 5f;
	float maxLeft = -5f; 
	float maxRight = 4f;
	float maxTop = 4f;
	float maxBottom = -5f;
	float sensibleBounds = 1f;

	void Start(){
		_camera = GameObject.Find ("Main Camera").transform;
	}


	void FixedUpdate(){
		if (Input.mousePosition.x > Screen.width - sensibleBounds) {
			//[move char right];
			MoveCamera('z', -movingSpeed);
		}
		if (Input.mousePosition.x < 0 + sensibleBounds) {
			MoveCamera('z', movingSpeed);
		}
		if (Input.mousePosition.y > Screen.height - sensibleBounds) {
			MoveCamera('x', movingSpeed);
		}
		if (Input.mousePosition.y < 0 + sensibleBounds) {
			MoveCamera('x', -movingSpeed);
		} 
	}

	void MoveCamera(char direction, float movingSpeed){
		float camMove = 0;
		if (direction.Equals('z')) {
			camMove = _camera.position.z + (movingSpeed * Time.deltaTime);
			camMove = Mathf.Clamp (camMove, maxLeft, maxRight);
			_camera.position = new Vector3(_camera.position.x, _camera.position.y, camMove);
		} 
		if (direction.Equals('x')) {
			camMove = _camera.position.x + (movingSpeed * Time.deltaTime);
			camMove = Mathf.Clamp (camMove, maxBottom, maxTop);
			_camera.position = new Vector3(camMove, _camera.position.y, _camera.position.z);
		}
	}

//	void OnMouseDown(){
//		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//		RaycastHit[] hits = Physics.RaycastAll (ray);
//		foreach (RaycastHit hit in hits) {
//			if (hit.collider.gameObject.layer.Equals(_layerMask)){
//				// we are at movable layer and the mouse is down
//				_mouseFirstPoint = Input.mousePosition; 
//			}
//		}
//
//	}

//	void OnMouseDrag() {
//		float deltaZ = Input.mousePosition.x - _mouseFirstPoint.x; 
//		float deltaX = Input.mousePosition.y - _mouseFirstPoint.y;
//		float camMoveZ = _camera.position.z + (deltaZ * Time.deltaTime * 0.1f);
//		float camMoveX = _camera.position.x + (deltaX * Time.deltaTime * 0.1f);
//		camMoveZ = Mathf.Clamp (camMoveZ, -5f, 4f);
//		camMoveX = Mathf.Clamp (camMoveX, -5f, 4f);
//
//		_camera.position = new Vector3(camMoveX, _camera.position.y, camMoveZ);
//	}

	// for mobile
	void OnTouchDrag(){
//		if (Input.touchCount > 0) 
//		{
//			Touch touch = Input.GetTouch(0);
//			Ray ray = Camera.main.ScreenPointToRay(touch.position);
//
//			if (touch.phase == TouchPhase.Moved) 
//			{
//				MoveCamera (); 
//			}        
//
//		}
	}

//	public float Boundary : int = 50;
//	public var speed : int = 5;
//
//	private var theScreenWidth : int;
//	private var theScreenHeight : int;
//
//	function Start() 
//	{
//		theScreenWidth = Screen.width;
//		theScreenHeight = Screen.height;
//	}
//
//	function Update() 
//	{
//		if (Input.mousePosition.x > theScreenWidth - Boundary)
//		{
//			transform.position.x += speed * Time.deltaTime;
//		}
//
//		if (Input.mousePosition.x < 0 + Boundary)
//		{
//			transform.position.x -= speed * Time.deltaTime;
//		}
//
//		if (Input.mousePosition.y > theScreenHeight - Boundary)
//		{
//			transform.position.y += speed * Time.deltaTime;
//		}
//
//		if (Input.mousePosition.y < 0 + Boundary)
//		{
//			transform.position.y -= speed * Time.deltaTime;
//		}
//
//	}    

}
