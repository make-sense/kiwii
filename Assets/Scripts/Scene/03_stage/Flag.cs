using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {

	private GameObject stageBtn;
	private float posX, posY;

	public void StickTheFlag(GameObject other)
	{
		stageBtn = GameObject.Find (other.name);
		posX = stageBtn.transform.localPosition.x;
		posY = stageBtn.transform.localPosition.y;

		transform.localPosition = new Vector3 (posX+15f, posY+50f, 0);
		this.gameObject.SetActive (true);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
