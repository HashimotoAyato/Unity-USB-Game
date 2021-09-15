using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewLimitWallManager : MonoBehaviour {

	private Vector3 WALLPOS = new Vector3 (0.0f,0.0f,100.0f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MoveViewLimitWall ();
	}

	void MoveViewLimitWall(){
		this.transform.position = USBManager.nowPosition + WALLPOS;
	}
}
