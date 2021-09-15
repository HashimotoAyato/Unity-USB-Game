using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : MonoBehaviour {

	private Vector3 CAMPOS = new Vector3(0.0f,6.0f,-14.0f); //USBの位置に対するMainCameraの相対的な位置

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		MoveMainCamera ();
	}

	void FixedUpdate(){

	}

	void MoveMainCamera(){
		this.transform.position = USBManager.nowPosition + CAMPOS;
	}
}
