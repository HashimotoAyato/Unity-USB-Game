using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

	public static int stageSelect = 0;

	private GameObject USBPort;
	private GameObject horizontalObstacle; 
	private GameObject verticalObstacle;
	private GameObject Popup;


	private GameObject guideBarPrefab;

	private GameObject[] guideBars = new GameObject[4];

	private readonly Vector3 guideBarSize = new Vector3 (1,1,200);

	public static readonly float 
		LIMIT_LEFT = -20.0f,
		LIMIT_RIGHT = 20.0f,
		LIMIT_UP = 20.0f,
		LIMIT_DOWN = -20.0f;

	// Use this for initialization
	void Start () {
		USBPort = GameObject.Find ("USBPort");
		horizontalObstacle = (GameObject)Resources.Load ("HorizontalObstacle");
		verticalObstacle = (GameObject)Resources.Load ("VerticalObstacle");
		Popup = (GameObject)Resources.Load ("Popup");

		guideBarPrefab = (GameObject)Resources.Load("GuideBar");
		MakeGuideBar ();
		MakeStage (stageSelect); //ステージの生成　引数でステージの選択ができる

		Debug.Log ("now stage is" + stageSelect);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		MoveGuideBars ();
	}

	void MakeGuideBar(){
		guideBars[0] = Instantiate (guideBarPrefab,new Vector3(LIMIT_UP,LIMIT_LEFT,0.0f),Quaternion.identity);
		guideBars[1] = Instantiate (guideBarPrefab,new Vector3(LIMIT_UP,LIMIT_RIGHT,0.0f),Quaternion.identity);
		guideBars[2] = Instantiate (guideBarPrefab,new Vector3(LIMIT_DOWN,LIMIT_LEFT,0.0f),Quaternion.identity);
		guideBars[3] = Instantiate (guideBarPrefab,new Vector3(LIMIT_DOWN,LIMIT_RIGHT,0.0f),Quaternion.identity);

		for (int i = 0; i < 4; i++)
			guideBars [i].transform.localScale = guideBarSize;
	}

	void MoveGuideBars(){
		guideBars [0].transform.position = new Vector3 (LIMIT_UP, LIMIT_LEFT, USBManager.nowPosition.z);
		guideBars [1].transform.position = new Vector3 (LIMIT_UP, LIMIT_RIGHT, USBManager.nowPosition.z);
		guideBars [2].transform.position = new Vector3 (LIMIT_DOWN, LIMIT_LEFT, USBManager.nowPosition.z);
		guideBars [3].transform.position = new Vector3 (LIMIT_DOWN, LIMIT_RIGHT, USBManager.nowPosition.z);
	}

	void MakeStage(int stageSelect){

		switch (stageSelect) {

		case -2:
			USBPort.transform.position = new Vector3 (0.0f, 10.0f, 75.0f); //ここのz座標の値が実質のステージの長さ
			Instantiate (Popup, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<PopupManager> ().//ここのz座標がPopUpのでる位置
			SetPopupText ("USBの移動",
				"方向キーを入力してUSBの移動ができます。\n" +
				"うまく移動してUSBをポートの挿しましょう。\n" +
				"(位置が合うと左上の矢印が赤色になります)");
			break;
		case -1:
			USBPort.transform.position = new Vector3 (0.0f, 0.0f, 75.0f);
			USBPort.transform.Rotate (new Vector3(0,0,1), 180.0f); //USBPortが上下反転
			Instantiate (Popup, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<PopupManager> ().//ここのz座標がPopUpのでる位置
			SetPopupText ("USBの回転",
				"気をつけて!!\n" +
				"USBの挿し口が逆になっています!\n" +
				"A、Sキーを押すとUSBが回転できます。\n" +
				"うまく回転させてUSBを挿しましょう。\n" +
				"(USBの角度が合うと左上の丸型の矢印が赤色になります)");
			break;
		case 0:
			USBPort.transform.position = new Vector3 (0.0f, 0.0f, 120.0f);
			Instantiate (Popup, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<PopupManager> ().
			SetPopupText ("障害物",
				"前方に見えるのは障害物です。\n" +
				"障害物にぶつかるとゲームオーバーです。\n" +
				"USBの回転の移動を駆使してUSBを挿し口に挿しましょう。\n" +
				"(これが最後のチュートリアルです)");
			Instantiate(verticalObstacle, new Vector3(0.0f,0.0f,50.0f), Quaternion.identity);
			break;
		case 1:
			USBManager.usbSpeedLevel = 1;
			USBPort.transform.position = new Vector3 (10.0f, 5.0f, 80.0f);
			USBPort.transform.Rotate (new Vector3(0,0,1),0.0f);
			break;
		case 2:
			USBManager.usbSpeedLevel = 1;
			USBPort.transform.position = new Vector3 (-7.0f, -5.0f, 140.0f);
			Instantiate (horizontalObstacle, new Vector3 (0.0f, -7.0f, 45.0f), Quaternion.identity);
			break;
		case 3:
			USBManager.usbSpeedLevel = 1;
			USBPort.transform.position = new Vector3 (10.0f, 0.0f, 140.0f);
			USBPort.transform.Rotate (new Vector3 (0, 0, 1), 180.0f);
			Instantiate (verticalObstacle, new Vector3 (5.0f, 0.0f, 45.0f), Quaternion.identity);
			break;
		case 4:
			USBManager.usbSpeedLevel = 2;
			USBPort.transform.position = new Vector3 (0.0f, 5.0f, 140.0f);
			USBPort.transform.Rotate (new Vector3(0,0,1), 90.0f);
			Instantiate (verticalObstacle, new Vector3 (5.0f, 0.0f, 45.0f), Quaternion.identity);
			break;
		case 5:
			USBManager.usbSpeedLevel = 2;
			USBPort.transform.position = new Vector3 (-10.0f, -5.0f, 140.0f);
			USBPort.transform.Rotate (new Vector3(0,0,1), -45.0f);
			Instantiate (horizontalObstacle, new Vector3 (5.0f, 0.0f, 60.0f), Quaternion.identity);
			break;
		case 6:
			USBManager.usbSpeedLevel = 2;
			USBPort.transform.position = new Vector3 (10.0f, 5.0f, 140.0f);
			USBPort.transform.Rotate (new Vector3(0,0,1),45.0f);
			Instantiate (verticalObstacle, new Vector3 (5.0f, 0.0f, 50.0f), Quaternion.identity).transform.Rotate(new Vector3(0,0,1),45.0f);
			break;
		case 7:
			USBManager.usbSpeedLevel = 3;
			USBPort.transform.position = new Vector3 (-15.0f, -5.0f, 160.0f);
			USBPort.transform.Rotate (new Vector3(0,0,1),150.0f);
			Instantiate (verticalObstacle, new Vector3 (5.0f, 0.0f, 40.0f), Quaternion.identity).transform.Rotate(new Vector3(0,0,1),45.0f);
			Instantiate (verticalObstacle, new Vector3 (5.0f, 0.0f, 100.0f), Quaternion.identity).transform.Rotate(new Vector3(0,0,1),45.0f);

			break;
		case 8:
			USBManager.usbSpeedLevel = 3;
			USBPort.transform.position = new Vector3 (5.0f, 5.0f, 160.0f);
			Instantiate (verticalObstacle, new Vector3 (0.0f, 0.0f, 40.0f), Quaternion.identity).transform.Rotate(new Vector3(0,0,1),-60.0f);
			Instantiate (verticalObstacle, new Vector3 (5.0f, 0.0f, 100.0f), Quaternion.identity).transform.Rotate(new Vector3(0,0,1),60.0f);
			break;
		case 9:
			USBManager.usbSpeedLevel = 3;
			USBPort.transform.position = new Vector3 (-12.0f, -5.0f, 180.0f);
			USBPort.transform.Rotate (new Vector3 (0, 0, 1), 180.0f);
			Instantiate (horizontalObstacle, new Vector3 (5.0f, 0.0f, 50.0f), Quaternion.identity);
			Instantiate (verticalObstacle, new Vector3 (5.0f, 0.0f, 110.0f), Quaternion.identity);
			break;
		case 10:
			USBManager.usbSpeedLevel = 4;
			USBPort.transform.position = new Vector3 (-10.0f, -5.0f, 170.0f);
			USBPort.transform.Rotate (new Vector3(0,0,1), 50.0f);
			Instantiate (verticalObstacle, new Vector3 (7.0f, 0.0f, 40.0f),Quaternion.identity);
			Instantiate (verticalObstacle, new Vector3 (-7.0f, 0.0f, 75.0f),Quaternion.identity);
			Instantiate (horizontalObstacle, new Vector3 (5.0f, 0.0f, 110.0f), Quaternion.identity);
			break;
		case 11:
			USBManager.usbSpeedLevel = 4;
			USBPort.transform.position = new Vector3 (3.0f, 7.0f, 200.0f);
			USBPort.transform.Rotate (new Vector3(0,0,1), -90.0f);
			Instantiate (horizontalObstacle, new Vector3 (0.0f, 15.0f, 45.0f), Quaternion.identity);
			Instantiate (horizontalObstacle, new Vector3 (0.0f, 12.0f, 70.0f), Quaternion.identity);
			Instantiate (horizontalObstacle, new Vector3 (0.0f, 9.0f, 95.0f), Quaternion.identity);
			Instantiate (horizontalObstacle, new Vector3 (0.0f, 6.0f, 120.0f), Quaternion.identity);
			break;
		case 12:
			USBManager.usbSpeedLevel = 4;
			USBPort.transform.position = new Vector3 (-10.0f, -5.0f, 280.0f);
			USBPort.transform.Rotate (new Vector3(0,0,1),180.0f);
			Instantiate (Popup, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<PopupManager> ().
			SetPopupText ("かんばって！！",
				"これが最後のステージです。\n" +
				"障害物が最高難度の配置になっています。\n" +
				"もしあなたがこのステージをクリア出来たなら\n" +
				"USBマスターの称号を与えます。");
			Instantiate (verticalObstacle, new Vector3 (10.0f, 0.0f, 45.0f), Quaternion.identity).transform.Rotate(new Vector3(0,0,1),10);
			Instantiate (verticalObstacle, new Vector3 (7.0f, 0.0f, 70.0f), Quaternion.identity).transform.Rotate(new Vector3(0,0,1),20);
			Instantiate (verticalObstacle, new Vector3 (4.0f, 0.0f, 95.0f), Quaternion.identity).transform.Rotate(new Vector3(0,0,1),30);
			Instantiate (verticalObstacle, new Vector3 (1.0f, 0.0f, 120.0f), Quaternion.identity).transform.Rotate(new Vector3(0,0,1),40);
			Instantiate (horizontalObstacle, new Vector3 (0.0f, 5.0f, 160.0f), Quaternion.identity).transform.Rotate(new Vector3(0,0,1),10);
			Instantiate (horizontalObstacle, new Vector3 (0.0f, 2.0f, 185.0f), Quaternion.identity).transform.Rotate(new Vector3(0,0,1),20);
			Instantiate (horizontalObstacle, new Vector3 (0.0f, -1.0f, 210.0f), Quaternion.identity).transform.Rotate(new Vector3(0,0,1),30);
			Instantiate (horizontalObstacle, new Vector3 (0.0f, -4.0f, 235.0f), Quaternion.identity).transform.Rotate(new Vector3(0,0,1),40);
			break;
		}

	}

}
