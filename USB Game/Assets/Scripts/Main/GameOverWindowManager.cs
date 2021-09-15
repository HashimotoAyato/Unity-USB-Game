using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindowManager : MonoBehaviour {

	[SerializeField]GameObject retryButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable(){
		retryButton.GetComponent<Button> ().Select();
	}
}
