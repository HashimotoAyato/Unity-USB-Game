using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBPortManager : MonoBehaviour {

	GameManager gm;
	USBManager um;
	GameObject headPosition;
	GameObject clearPosition;
	GameObject USB;

	const float verticalClearSpace = 0.3001f;
	const float horizontalClearSpace = 0.5001f;
	const float clearRotateSpace = 4.001f;



	// Use this for initialization
	void Start () {
		clearPosition = GameObject.Find("ClearPosition");
		gm = GameObject.Find ("MyGameManager").GetComponent<GameManager> ();
		um = GameObject.Find ("USB").GetComponent<USBManager>();
		headPosition = GameObject.Find ("HeadPosition");
		USB = GameObject.Find ("USB");
	}
	
	// Update is called once per frame
	void Update () {
		if(gm.NowPlay()){ //プレイ中の処理
			
			if (clearPosition.transform.position.z < headPosition.transform.position.z) { //USBの先端のz座標がUSBポートの先端のz座標に達したの判断
				if (IsClearVertical() && IsClearHorizontal()){
					if (IsClearRotate()){ //USBのz軸の回転が許容範囲内であるかの判断
						gm.SetClearMovie (); //clear movie
					}else{
						um.PlayCollisionSE ();
						gm.SetGameOverMovie (); //game over movie
					}
				} else {
					um.PlayCollisionSE ();
					gm.SetGameOverMovie (); //game over movie
				}

			}
		}
	}

	public bool IsClearRotate(){ //USBのz軸の回転が許容範囲内であるかを判断する関数

		float x1 = this.transform.localEulerAngles.z - clearRotateSpace,
			x2 = this.transform.localEulerAngles.z + clearRotateSpace,
			USBRotate = USB.transform.localEulerAngles.z;

		if (x1 < 0) { //許容範囲を含めた回転角度が0未満である場合
			
			if (((x1 + 360.0f) < USBRotate && USBRotate < 360) || (0 <= USBRotate && USBRotate < x2))
				return true;
			
		} else if (x2 >= 360) { //許容範囲を含めた回転角度が360以上である場合

			if ((x1 < USBRotate && USBRotate < 360) || (0 <= USBRotate && USBRotate < (x2 - 360)))
				return true;

		} else { //その他の場合
			
			if (x1 < USBRotate && USBRotate < x2)
				return true;

		}

		return false; //許容範囲外
	}

	public bool IsClearVertical(){
		if ((clearPosition.transform.position.y - verticalClearSpace) < headPosition.transform.position.y &&
		    (clearPosition.transform.position.y + verticalClearSpace) > headPosition.transform.position.y)
			return true;
		else
			return false;
	}

	public bool IsClearHorizontal(){
		if ((clearPosition.transform.position.x - horizontalClearSpace) < headPosition.transform.position.x && //USBのxy座標が許容範囲内であるかの判断 
		    (clearPosition.transform.position.x + horizontalClearSpace) > headPosition.transform.position.x)
			return true;
		else
			return false;
	}

	public float FindPoint(){
		float hp = CalcHorizontalPoint (),
		vp = CalcVerticalPoint (),
		rp = CalcRotatePoint ();
		Debug.Log ("hp = " + hp);
		Debug.Log ("vp = " + vp);
		Debug.Log ("rp = " + rp);
		return hp + vp + rp;
	}

	float CalcHorizontalPoint(){
		float dif;

		dif = headPosition.transform.position.x - (clearPosition.transform.position.x - horizontalClearSpace);
		if ((clearPosition.transform.position.x + horizontalClearSpace) - headPosition.transform.position.x < dif)
			dif = (clearPosition.transform.position.x + horizontalClearSpace) - headPosition.transform.position.x;

		return 100.0f * (dif / horizontalClearSpace);

	}

	float CalcVerticalPoint(){
		float dif;

		dif = headPosition.transform.position.y - (clearPosition.transform.position.y - verticalClearSpace);
		if ((clearPosition.transform.position.y + verticalClearSpace) - headPosition.transform.position.y < dif)
			dif = (clearPosition.transform.position.y + verticalClearSpace) - headPosition.transform.position.y;

		return 100.0f * (dif / verticalClearSpace);
		
	}

	float CalcRotatePoint(){
		float x1 = this.transform.localEulerAngles.z - clearRotateSpace,
		x2 = this.transform.localEulerAngles.z + clearRotateSpace,
		USBRotate = USB.transform.localEulerAngles.z;

		float dif = 0.0f;

		if (x1 < 0) { //許容範囲を含めた回転角度が0未満である場合

			if ((x1 + 360.0f) < USBRotate) {
				dif = USBRotate - (x1 + 360.0f);
				if (dif > x2 - (360.0f - USBRotate))
					dif = x2 - (360.0f - USBRotate);
			} else if (0 <= USBRotate && USBRotate < x2) {
				dif = x2 - USBRotate;
				if (dif > (USBRotate + 360.0f) - x1)
					dif = (USBRotate + 360.0f) - x1;
			}

		} else if (x2 >= 360) { //許容範囲を含めた回転角度が360以上である場合

			if (x1 < USBRotate){
				dif = USBRotate - x1;
				if (dif > x2 - (360.0f - USBRotate))
					dif = x2 - (360.0f - USBRotate);
			}else if(USBRotate < (x2 - 360.0f)){
				dif = x2 - USBRotate;
				if (dif > (USBRotate + 360.0f) - x1)
					dif = (USBRotate + 360.0f) - x1;
			}

		} else { //その他の場合

			dif = USBRotate - x1;
			if (dif > x2 - USBRotate)
				dif = x2 - USBRotate;

		}

		return 100.0f * (dif / clearRotateSpace);
	}

}
