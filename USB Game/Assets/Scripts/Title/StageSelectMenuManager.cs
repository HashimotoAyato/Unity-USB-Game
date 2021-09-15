using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectMenuManager : MonoBehaviour {

	[SerializeField] GameObject FirstMenu;
	[SerializeField] TitleManager tm;
	[SerializeField] Button[] stages; 

	// Use this for initialization
	void Start () {
		stages [0].Select ();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.activeSelf && (Input.GetKey (KeyCode.B) || Input.GetButton ("PS3batsubutton"))){
			tm.PlayBackSound ();
			FirstMenu.SetActive (true);
			this.gameObject.SetActive (false);
		}
	}

	void OnEnable(){
		stages [0].Select ();
	}
}
