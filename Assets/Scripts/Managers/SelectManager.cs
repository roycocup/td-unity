using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 

public class SelectManager : MonoBehaviour {

	public Material _red;

	GameObject _go;
	GameObject _previousObject; 
	Material _goOriginalMaterial; 
	int _layerMask = 8;
	bool _selected = false;

	void FixedUpdate () {
		_go = getHoveringObject ();
		if (_go != null) {
			_previousObject = _go;
			Select (_go);
		} else {
			if (_previousObject != null) {
				UnselectPrevious ();
			}
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
		_selected = true;
	}


	void UnselectPrevious(){
		Debug.Log (_goOriginalMaterial.mainTexture.name);
		MeshRenderer mr = _previousObject.GetComponent<MeshRenderer> ();
		mr.material = _goOriginalMaterial; 
		_previousObject = null;
		_selected = false;
	}

}
