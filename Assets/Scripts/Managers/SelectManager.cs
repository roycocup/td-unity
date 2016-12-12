using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 

public class SelectManager : MonoBehaviour {

	public Material red;
	public Material white;


	GameObject _go;
	GameObject _previousObject; 
	Material _goOriginalMaterial; 
	int _layerMask = 8;
	bool _selected = false;
	UIManager _uiManager; 

	struct MaterialRegistry{
		public Material material; 
	}


	void Start(){
		_uiManager = gameObject.GetComponent<UIManager> ();
	}

	void FixedUpdate () {

		// look for things under the mouse
		_go = getHoveringObject ();

		// this may be a tower or a towerblock
		// so it needs to be handled differently
		if (_go != null) {
			// set the record for the previous object
			_previousObject = _go;
			string t = GetTagForGO (_go);
			if (t != null) {
				if (t == "Tower") {
					//TODO: do somehting with towers
					ShowTowerUI(_go); 
				} else {
					// if this is clickable and its not a tower
					Debug.Log(_go.tag); 
					Highlight (_go);
				}
			}

		} else {
			if (_previousObject != null) {
				RemoveHighlight ();
			}
		}
	}

	void ShowTowerUI(GameObject go){
		//uiManager
	}


	string GetTagForGO (GameObject _go){
		
		if (_go.tag != "Untagged") {
			return _go.tag;
		} else {
			if (null != _go.transform.parent) {

				//FIXME: This is so terrible I cannot begin.... but I cant find an answer and have to move on.
				return _go.transform.parent.tag;
				//return GetTagForGO (_go.transform.parent.gameObject);
			} 
			return null;
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
		mr.material = red; 
		_selected = true;
	}


	void RemoveHighlight(){
		MeshRenderer mr = _previousObject.GetComponent<MeshRenderer> ();
		mr.material = white; 
		_previousObject = null;
		_selected = false;
	}

}
