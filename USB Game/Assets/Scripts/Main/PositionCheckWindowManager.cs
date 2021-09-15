using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCheckWindowManager : MonoBehaviour {

	[SerializeField] GameObject rotateCheck;
	[SerializeField] GameObject verticalCheck;
	[SerializeField] GameObject horizonCheck;

	private USBPortManager upm;
	// Use this for initialization
	void Start () {
		upm = GameObject.Find ("USBPort").GetComponent<USBPortManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (upm.IsClearRotate ()) //回転角が許容範囲内
			rotateCheck.SetActive (true);
		else
			rotateCheck.SetActive (false);

		if (upm.IsClearHorizontal ()) //水平方向の座標が許容範囲内
			horizonCheck.SetActive (true);
		else
			horizonCheck.SetActive (false);

		if (upm.IsClearVertical ()) //垂直方向の座標が許容範囲内
			verticalCheck.SetActive (true);
		else
			verticalCheck.SetActive (false);
		
	}
}
