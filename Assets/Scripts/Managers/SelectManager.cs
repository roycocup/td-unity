using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectManager : MonoBehaviour {

	public Material _red;

	GameObject _go; 
	Material _goOriginalMaterial; 


	void Update () {
		_go = getHoveringObject ();
		if (_go != null) {
			Select (_go);
		}
	}


	GameObject getHoveringObject(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll (ray);
		// TODO: only execute if UI is not enabled
		if (hits.Length > 0){
			if (hits[0].collider.gameObject.tag == "Clickable") {
				return hits[0].collider.gameObject;
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
		_goOriginalMaterial = mr.material; 
		mr.material = _red; 
	}

}
