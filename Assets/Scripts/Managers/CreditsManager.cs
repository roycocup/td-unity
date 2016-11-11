using UnityEngine;
using System.Collections;

public class CreditsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Cancel")) {
			Application.Quit ();
		}
	}


}
