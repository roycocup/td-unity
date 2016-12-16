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
	UpgradeManager _upgradeManager;

	struct MaterialRegistry{
		public Material material; 
	}

	void Start(){
		_upgradeManager = GameObject.Find ("GameManager").GetComponent<UpgradeManager> ();
	}

	void FixedUpdate () {

		// look for things under the mouse
		_go = getHoveringObject ();

		// this may be a tower or a towerblock
		// so it needs to be handled differently
		if (_go != null) {
			// set the record for the previous object
			_previousObject = _go;
			string tag = GetTagForGO (_go);
			if (tag != null) {
				Highlight (_go, tag);
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

	string GetTagForGO (GameObject _go){
		if (_go.tag != "Untagged") {
			return _go.tag;
		} 
		return null;
	}

	void Highlight(GameObject go, string tag){
		switch (tag) {
		case "Tower":
			GameObject select = go.transform.Find ("select").gameObject;
			select.SetActive (true);
			if (Input.GetMouseButton(0)) {
				// the reason why we are calling the upgrade manager instead of the UIManager just to display the menu
				// is because we need to persist the tower that requires upgrade
				_upgradeManager.DisplayUpgradeTowerMenu (go);
			}
			break;
		case "Spots":
			MeshRenderer mr = go.GetComponent<MeshRenderer> ();
			mr.material = red;
			break;
		}

	}


	void RemoveHighlight(){
		string t = GetTagForGO (_previousObject);
		switch (t) {
		case "Tower":
			GameObject select = _previousObject.transform.Find ("select").gameObject;
			select.SetActive (false);
			break;
		case "Spots":
			MeshRenderer mr = _previousObject.GetComponent<MeshRenderer> ();
			mr.material = white; 
			break;
		}

		// and then nullify the register
		_previousObject = null;

	}

}
