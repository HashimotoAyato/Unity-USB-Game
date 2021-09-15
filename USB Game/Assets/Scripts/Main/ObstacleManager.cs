using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

	const float DESTROY_DISTANCE = 5.0f;

	private float thisposition;

	// Use this for initialization
	void Start () {
		thisposition = this.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (USBManager.nowPosition.z - DESTROY_DISTANCE > thisposition)
			Destroy (this.gameObject);
	}
}
