using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectManager : MonoBehaviour {

	public Material _red;

	GameObject _go; 
	Material _goOriginalMaterial; 
	int _layerMask = 8;

	void Start(){
		Debug.Log (_layerMask); 
	}


	void FixedUpdate () {
		_go = getHoveringObject ();
		if (_go != null) {
			Select (_go);
		}
	}


	GameObject getHoveringObject(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray);
		//RaycastHit[] hits = Physics.RaycastAll (Input.mousePosition, Vector3.forward, Mathf.Infinity);
		// TODO: only execute if UI is not enabled
		if (hits.Length > 0){
			foreach (RaycastHit h in hits) {
				if (h.collider.gameObject.layer.Equals(8)){
					return h.collider.gameObject;	
				}
			}

		}
		return null;
	}

	void Select(GameObject go){
		MeshRenderer mr = go.GetComponent<MeshRenderer> ();
		_goOriginalMaterial = mr.material; 
		mr.material = _red; 
	}


	void Unselect(GameObject go){
		MeshRenderer mr = go.GetComponent<MeshRenderer> ();
		mr.material = _goOriginalMaterial; 
	}

}
