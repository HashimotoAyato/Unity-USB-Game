using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBManager : MonoBehaviour {

	GameManager gm;
	Rigidbody rb;
	GameObject lastPosition, clearPosition;
	[SerializeField]GameObject USBGuideLine;
	[SerializeField]AudioClip CollisionSE;

	private float[] FORWARD_SPEED = {0.15f,0.18f,0.21f,0.24f,0.27f}; //USBの前進速度
	const float SLOW_FORWARD_SPEED = 0.03f; //スローモーション時のUSBの前進速度
	const float VERTICAL_SPEED = 0.2f; //USBの水平方向の移動速度
	const float HORIZONTAL_SPEED = 0.2f; //USBの垂直方向の移動速度
	const float ROTATE_SPEED = 2.0f; //USBの回転速度
	const float REJECT_FORCE = 500.0f; //USBを跳ね返す際の力
	const float CLEAR_SPACE_TIME = 0.8f; //USBが挿さりきってからクリア画面が表示されるまでの時間
	const float USB_INTO_TIME = 3.0f; //USBを挿入するのにかかる時間
	const float LIMIT_SPACE = 3.0f; //移動限界（壁）との余裕

	private float time;
	private float distance;
	public static int usbSpeedLevel;

	private bool leftRotate, 
				rightRotate, 
				leftMove, 
				rightMove,
				upMove,
				downMove; //キー入力の判定

	public static Vector3 nowPosition;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("MyGameManager").GetComponent<GameManager>();
		rb = this.GetComponent<Rigidbody> ();
		lastPosition = GameObject.Find("LastPosition");
		clearPosition = GameObject.Find ("ClearPosition");

		time = 0;
		distance = 0.0f;

		nowPosition = this.transform.position;

		leftRotate = false;
		rightRotate = false;
		leftMove = false;
		rightMove = false;
		upMove = false;
		downMove = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (gm.NowPlay ()) { //プレイ中のみ操作可能
			if (Input.GetKey (KeyCode.A) || Input.GetButton("PS3l1button"))
				leftRotate = true;
				
			if (Input.GetKey (KeyCode.S) || Input.GetButton("PS3r1button"))
				rightRotate = true;
				
			if ((Input.GetKey (KeyCode.LeftArrow) || Input.GetButton("PS3leftarrow")) && nowPosition.x > StageManager.LIMIT_LEFT + LIMIT_SPACE)
				leftMove = true;

			if ((Input.GetKey (KeyCode.RightArrow) || Input.GetButton("PS3rightarrow")) && nowPosition.x < StageManager.LIMIT_RIGHT - LIMIT_SPACE)
				rightMove = true;
			
			if (( Input.GetKey (KeyCode.UpArrow) || Input.GetButton("PS3uparrow")) && nowPosition.y < StageManager.LIMIT_UP - LIMIT_SPACE)
				upMove = true;
			
			if ((Input.GetKey (KeyCode.DownArrow) || Input.GetButton("PS3downarrow")) && nowPosition.y > StageManager.LIMIT_DOWN + LIMIT_SPACE)
				downMove = true;
		}

		if (gm.NowPlay ())
			USBGuideLine.SetActive (true);
		else
			USBGuideLine.SetActive (false);

		if (gm.NowClearMovie ()) {
			if (distance == 0.0f)
				distance = clearPosition.transform.position.z - lastPosition.transform.position.z;
			
			if (lastPosition.transform.position.z < clearPosition.transform.position.z) {
				this.transform.position += new Vector3 (0.0f, 0.0f, Time.deltaTime * distance / USB_INTO_TIME);
				time += Time.deltaTime;
			} else if (time < CLEAR_SPACE_TIME + USB_INTO_TIME)
				time += Time.deltaTime;
			else {
				gm.SetGameClear ();
				time = 0.0f;
			}
		}
	}

	void FixedUpdate(){
		
		if (gm.NowPlay ()) { //プレイ中のみ移動可能

			MoveForward ();
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

			nowPosition = this.transform.position;

/*		} else if (gm.NowClearMovie ()) {
			if (lastPosition.transform.position.z < clearPosition.transform.position.z) {
				this.transform.position += new Vector3(0.0f,0.0f,time * (clearPosition.transform.position.z - lastPosition.transform.position.z) / USB_INTO_TIME);
				time += Time.deltaTime;
			}else if (time < CLEAR_SPACE_TIME + USB_INTO_TIME)
				time += Time.deltaTime;
			else {
				gm.SetGameClear ();
				time = 0.0f;
			}*/
		} else if (gm.NowGameOverMovie ()){
			nowPosition = this.transform.position;
		}

	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Obstacle") {
			if (!gm.NowGameOverMovie ()) {
				PlayCollisionSE ();
				gm.SetGameOverMovie ();
			}
		}
	}

	void MoveForward(){ //前進
		this.transform.position += new Vector3(0.0f,0.0f,FORWARD_SPEED[usbSpeedLevel]);
	}

	void SlowMoveForward(){
		this.transform.position += new Vector3 (0.0f,0.0f,SLOW_FORWARD_SPEED);
	}

	void LeftRotate(){ //左回転
		this.transform.Rotate(new Vector3 (0,0,1),ROTATE_SPEED);
	}

	void RightRotate(){ //右回転
		this.transform.Rotate(new Vector3 (0,0,1),-ROTATE_SPEED);
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

	public void RejectUSB(){
		rb.useGravity = true;
		rb.AddForce (new Vector3(0.0f,0.0f,-REJECT_FORCE));
	}

	public void PlayCollisionSE(){
		GetComponent<AudioSource>().PlayOneShot(CollisionSE);
	}
}
