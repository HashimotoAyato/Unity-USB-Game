using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUSBManager : MonoBehaviour {

	private const float GLOBAL_ROTATE_SPEED = 1.0f;
	private const float LOCAL_ROTATE_SPEED = 1.5f;
	private const float VERTICAL_SPEED = 0.1f;
	private const float HORIZONTAL_SPEED = 0.1f;

	private const float HORIZONTAL_LIMIT = 8f;
	private const float VERTICAL_LIMIT = 5f;

	private bool leftRotate, 
				 rightRotate, 
				 leftMove, 
				 rightMove,
				 upMove,
				 downMove;

	//[SerializeField]private GameObject menuWindow;
	[SerializeField]private GameObject usbModel;

	// Use this for initialization
	void Start () {
		leftRotate = false;
		rightRotate = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A) || Input.GetButton("PS3l1button"))
			leftRotate = true;
		if (Input.GetKey (KeyCode.S) || Input.GetButton("PS3r1button"))
			rightRotate = true;
		if (TitleManager.nowTitle) {
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetButton ("PS3leftarrow"))
				leftMove = true;
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetButton ("PS3rightarrow"))
				rightMove = true;
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetButton ("PS3uparrow"))
				upMove = true;
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetButton ("PS3downarrow"))
				downMove = true;
		}
	}

	void FixedUpdate(){

		DefaultRotate ();

		if (leftRotate) {
			LeftRotate ();
			leftRotate = false;
		}
		if (rightRotate) {
			RightRotate ();
			rightRotate = false;
		}
		if (leftMove) {
			MoveLeft ();
			leftMove = false;
		}
		if (rightMove) {
			MoveRight ();
			rightMove = false;
		}
		if (upMove) {
			MoveUp ();
			upMove = false;
		}
		if (downMove) {
			MoveDown ();
			downMove = false;
		}

		TransportLimit ();
	}

	void LeftRotate(){
		this.usbModel.transform.Rotate (new Vector3(1,0,0), LOCAL_ROTATE_SPEED);
	}

	void RightRotate(){
		this.usbModel.transform.Rotate (new Vector3(1,0,0), -LOCAL_ROTATE_SPEED);
	}

	void DefaultRotate(){
		this.transform.Rotate (new Vector3(0,1,0), GLOBAL_ROTATE_SPEED);
	}

	void MoveLeft(){ //左移動
		this.transform.position -= new Vector3 (VERTICAL_SPEED,0.0f,0.0f);
	}

	void MoveRight(){ //右移動
		this.transform.position += new Vector3 (VERTICAL_SPEED,0.0f,0.0f);
	}

	void MoveUp(){ //上移動
		this.transform.position += new Vector3 (0.0f,HORIZONTAL_SPEED,0.0f);
	}

	void MoveDown(){ //下移動
		this.transform.position -= new Vector3 (0.0f,HORIZONTAL_SPEED,0.0f);
	}

	void TransportLimit(){
		Vector3 pos = this.transform.position;
		if (Mathf.Abs (pos.x) > HORIZONTAL_LIMIT) {
			this.transform.position= new Vector3(-Mathf.Sign(pos.x)*(HORIZONTAL_LIMIT*2 - Mathf.Abs(pos.x)), pos.y, pos.z);
		}
		if (Mathf.Abs (pos.y) > VERTICAL_LIMIT) {
			this.transform.position= new Vector3(pos.x, -Mathf.Sign(pos.y)*(VERTICAL_LIMIT*2 - Mathf.Abs(pos.y)), pos.z);
		}
	}
}
