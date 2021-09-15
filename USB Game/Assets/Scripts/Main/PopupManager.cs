using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PopupManager : MonoBehaviour {

	private GameManager gm;
	//private Button firstSelectBotton;
	private string title, contents;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("MyGameManager").GetComponent<GameManager>();
		//firstSelectBotton = GameObject.Find ("OKbutton").GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gm.NowPlay()) {
			if (!gm.GetPopupActive() && (USBManager.nowPosition.z > this.transform.position.z)) {
				gm.SetPopupText(title, contents);
				gm.SetPopup ();
				gm.ActivePopup();
				DestroyPopup ();
			}
		}
	}

	void OnEnable(){
		
	}

	public void SetPopupText(string t, string s){
		title = t;
		contents = s;
	}

	void DestroyPopup(){
		Destroy (this.gameObject);
	}
}
