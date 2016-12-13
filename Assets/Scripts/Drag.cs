using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {

	float _maxLeft = 12f;
	float _maxRight = 12f;
	Transform _camera; 
	Vector3 _originalCameraPoint; 
	Vector3 _mouseFirstPoint; 
	int _layerMask = 9;

	void Start(){
		_camera = GameObject.Find ("Main Camera").transform;
		_originalCameraPoint = _camera.transform.position; 
	}


	void OnMouseDown(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll (ray);
		foreach (RaycastHit hit in hits) {
			if (hit.collider.gameObject.layer.Equals(_layerMask)){
				// we are at movable layer and the mouse is down
				_mouseFirstPoint = Input.mousePosition; 
			}
		}

	}

	void OnMouseDrag() {
		float deltaZ = Input.mousePosition.x - _mouseFirstPoint.x; 
		float deltaX = Input.mousePosition.y - _mouseFirstPoint.y;
		float camMoveZ = _camera.position.z + (deltaZ * Time.deltaTime * 0.1f);
		float camMoveX = _camera.position.x + (deltaX * Time.deltaTime * 0.1f);
		camMoveZ = Mathf.Clamp (camMoveZ, -5f, 4f);
		camMoveX = Mathf.Clamp (camMoveX, -5f, 4f);

		_camera.position = new Vector3(camMoveX, _camera.position.y, camMoveZ);
	}

	// for mobile
	void OnTouchDrag(){
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.GetTouch(0);
			Ray ray = Camera.main.ScreenPointToRay(touch.position);

			if (touch.phase == TouchPhase.Moved) 
			{
				MoveCamera (); 
			}        

		}
	}

	void MoveCamera(){
		
	}
	



}
