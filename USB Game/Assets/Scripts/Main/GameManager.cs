using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private USBManager um;
	private GameObject up;
	[SerializeField] GameObject countDownText;
	[SerializeField] GameObject USBPortCamera;
	[SerializeField] GameObject clearMovieCamera;
	[SerializeField] GameObject PositionCheckWindow;
	[SerializeField] GameObject popupWindow;
	[SerializeField] GameObject tutorialClear;
	[SerializeField] GameObject stageClear;
	[SerializeField] GameObject gameOverWindow;
	[SerializeField] AudioClip tutorialBGM;
	[SerializeField] AudioClip stageBGM;
	[SerializeField] AudioSource BGM;
	[SerializeField] AudioSource SE;
	[SerializeField] Text popupText;
	[SerializeField] Text popupTitle;

	const int countDownSec = 3; //プレイ開始前のカウントダウンする秒数
	const float countSec = 1.0f;
	const float USB_PORT_NEAR_DISTANCE = 80.0f;
	const float WAIT_TIME = 0.5f; //チャタリング防止用待機時間
	const float GAMEOVER_SPACE_TIME = 1.0f; //ゲームオーバーになってからwindowが表示されるまでの時間
	private float time;
	private int count;

	public enum Status{
		standby,
		play,
		gameover,
		gameclear,
		pause,
		popup,
		clearMovie,
		gameOverMovie
	}

	private Status nowStatus;

	// Use this for initialization
	void Start () {
		um = GameObject.Find ("USB").GetComponent<USBManager> ();
		up = GameObject.Find ("USBPort");
		nowStatus = Status.standby;
		time = 0.0f;
		count = countDownSec;
		Debug.Log ("gamestart");
		if (StageManager.stageSelect > 0)
			BGM.clip = stageBGM;
		else
			BGM.clip = tutorialBGM;
		BGM.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (nowStatus) {
		case Status.standby:
			
			time += Time.deltaTime;

			if (count < 0)
				nowStatus = Status.play;
			

			if (time > countSec) {
				countDownText.SetActive (true);
				countDownText.GetComponent<Text> ().text = count == 0 ? "GO" : count.ToString ();
				count--;
				time = 0.0f;
			}

			break;
		case Status.play: //プレイ中に行う処理の記述
			if (up.transform.position.z - USB_PORT_NEAR_DISTANCE < USBManager.nowPosition.z) {
				USBPortCamera.SetActive (true);
				PositionCheckWindow.SetActive (true);
			}

			break;
		case Status.gameover: //ゲームオーバーの際に実行する処理の記述
			gameOverWindow.SetActive(true);
			break;
		case Status.gameclear: //ゲームクリアの際に実行する処理の記述
			if (!tutorialClear.activeSelf && !stageClear.activeSelf) {
				if (StageManager.stageSelect > 0) {
					stageClear.SetActive (true);
				} else {
					tutorialClear.SetActive (true);
				}
			}
			break;
		case Status.pause:

			break;
		case Status.popup: //ポップアップ中に行う処理

			break;
		case Status.clearMovie:
			if(USBPortCamera.activeSelf)
				USBPortCamera.SetActive (false);
			if(!clearMovieCamera.activeSelf)
				clearMovieCamera.SetActive (true);
			break;
		case Status.gameOverMovie:
			time += Time.deltaTime;
			if (time > GAMEOVER_SPACE_TIME)
				nowStatus = Status.gameover;
			break;
		}


	}

	public bool NowPlay(){ //プレイ中かの判定
		if (nowStatus == Status.play)
			return true;
		else
			return false;
	}

	public bool NowGameClear(){
		if (nowStatus == Status.gameclear)
			return true;
		else
			return false;
	}

	public bool NowGameOver(){
		if (nowStatus == Status.gameover)
			return true;
		else
			return false;
	}

	public void SetGameOver(){ //ゲームオーバーの状態にセットする
		
		nowStatus = Status.gameover;
		Debug.Log ("gameover");
	}

	public void SetGameClear(){ //ゲームクリアの状態にセットする
		nowStatus = Status.gameclear;
		Debug.Log ("gameclear");
	}

	public void SetPopup(){
		nowStatus = Status.popup;
		Debug.Log ("pupup");
	}

	public void SetPlay(){
		nowStatus = Status.play;
		Debug.Log ("play");
	}

	public void ActivePopup(){
		popupWindow.SetActive (true);
	}

	public void SetPopupText(string t, string s){
		popupTitle.text = t;
		popupText.text = s;
	}

	public bool GetPopupActive(){
		return popupWindow.activeSelf;
	}

	public void SetClearMovie(){
		nowStatus = Status.clearMovie;
	}

	public bool NowClearMovie(){
		if (nowStatus == Status.clearMovie)
			return true;
		else
			return false;
	}

	public void SetGameOverMovie(){
		um.RejectUSB();
		nowStatus = Status.gameOverMovie;
	}

	public bool NowGameOverMovie(){
		if (nowStatus == Status.gameOverMovie)
			return true;
		else
			return false;
	}

	public void LoadNextStage(){
		StageManager.stageSelect++;
		SceneManager.LoadScene ("MainScene");
	}

	public void ReturnTitle(){
		SceneManager.LoadScene ("Title");
	}

	public void Retry(){
		SceneManager.LoadScene ("MainScene");
	}
}
