using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTextControler : MonoBehaviour {

	const float fadeSec = 0.7f; //フェードアウトにかかる秒数
	private float time;

	// Use this for initialization
	void Start () {
		time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		this.GetComponent<Text> ().color = new Color(this.GetComponent<Text> ().color.r,
													this.GetComponent<Text> ().color.g,
													this.GetComponent<Text> ().color.b,
													1.0f - (time/fadeSec) );
		if (time > fadeSec)
			this.gameObject.SetActive (false);
			
	}

	void OnEnable(){
		time = 0.0f;
	}
}
