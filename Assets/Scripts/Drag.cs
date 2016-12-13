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
		float deltaMove = Input.mousePosition.x - _mouseFirstPoint.x; 
		float cameraMove = _camera.position.z + (deltaMove * Time.deltaTime * 0.1f);
		cameraMove = Mathf.Clamp (cameraMove, -5f, 4f);
		_camera.position = new Vector3(_camera.position.x, _camera.position.y, cameraMove);
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
