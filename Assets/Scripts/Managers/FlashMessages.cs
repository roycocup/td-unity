using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class FlashMessages : MonoBehaviour {

	string _message;
	GameObject _messageUI; 
	Text _textUI; 
	float _flashtime = 3f; 
	float _timeElapsed = 0f; 
	bool _messageSet = false;

	public string Message {
		get {
			return _message;
		}
		set {
			_message = value;
			_messageSet = true;
		}
	}

	void Start () {
		_messageUI = GameObject.Find ("UI/Canvas/FlashMessages"); 
		_textUI = _messageUI.GetComponentInChildren<Text>();
	}

	void Update () {
		if (_messageSet == true) {
			if (_timeElapsed < _flashtime) {
				if (_messageUI.activeSelf == false) {
					_messageUI.SetActive (true); 
					_textUI.text = _message;
				}
				_timeElapsed += Time.deltaTime; 
			} else {
				_messageUI.SetActive (false); 
				_textUI.text = null; 
				_messageSet = false; 
				_timeElapsed = 0; 
			}
		}
	}
}
