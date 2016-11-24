using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {


	void Start () {
	
	}
	

	void FixedUpdate () {

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
