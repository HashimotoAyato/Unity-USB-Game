using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearStageManager : MonoBehaviour {

	[SerializeField]GameObject nextButton;
	[SerializeField]GameObject returnTitleButton;

	const int LAST_STAGE_NUMBER = 12;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnEnable(){
		Debug.Log ("nowStage is" + StageManager.stageSelect);
		if (StageManager.stageSelect == LAST_STAGE_NUMBER) {
			Debug.Log ("hashimoto");
			returnTitleButton.GetComponent<Button> ().Select ();
			nextButton.SetActive (false);
		} else {
			nextButton.GetComponent<Button> ().Select ();
		}
	}
}
