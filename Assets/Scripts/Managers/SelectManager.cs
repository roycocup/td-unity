using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 

public class SelectManager : MonoBehaviour {

	public Material _red;
	public Material _white;

	GameObject _go;
	GameObject _previousObject; 
	Material _goOriginalMaterial; 
	int _layerMask = 8;
	bool _selected = false;

	struct MaterialRegistry{
		public Material material; 
	}

	void FixedUpdate () {

		// look for things under the mouse
		_go = getHoveringObject ();

		// this may be a tower or a towerblock
		// so it needs to be handled differently
		if (_go != null) {
			// set the record for the previous object
			_previousObject = _go;
			if (_go.tag == "Tower") {
				//TODO: do somehting with towers
			} else {
				// if this is clickable and its not a tower
				Highlight (_go);
			}

		} else {
			if (_previousObject != null) {
				RemoveHighlight ();
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
				// if the object is in the clickable layer mask
				if (h.collider.gameObject.layer.Equals(_layerMask)){
					GameObject go = h.collider.gameObject; 
					return go;
				}
			}
		}
		return null;
	}

	void Highlight(GameObject go){
		MeshRenderer mr = go.GetComponent<MeshRenderer> ();
//		_matReg.material = mr.material;
		mr.material = _red; 
		_selected = true;
	}


	void RemoveHighlight(){
		MeshRenderer mr = _previousObject.GetComponent<MeshRenderer> ();
		mr.material = _white; 
		_previousObject = null;
		_selected = false;
	}

}
