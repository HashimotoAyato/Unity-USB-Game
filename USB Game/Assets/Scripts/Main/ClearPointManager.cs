using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearPointManager : MonoBehaviour {

	private Text clearPointText;
	private USBPortManager upm;
	private float point;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake(){
		clearPointText = this.GetComponent<Text> ();
		upm = GameObject.Find ("USBPort").GetComponent<USBPortManager> ();
	}

	void OnEnable(){
		float point = upm.FindPoint ();

		if (point > 280.0f)
			clearPointText.text = ("USBの挿し方：★★★★★");
		else if (point > 230.0f)
			clearPointText.text = ("USBの挿し方：★★★★☆");
		else if (point > 170.0f)
			clearPointText.text = ("USBの挿し方：★★★☆☆");
		else if(point > 110.0f)
			clearPointText.text = ("USBの挿し方：★★☆☆☆");
		else
			clearPointText.text = ("USBの挿し方：★☆☆☆☆");

		clearPointText.text += ("\n\n点数：" + (int)(point+1.0f) + " / 300");
	}
}
