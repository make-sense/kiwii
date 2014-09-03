using UnityEngine;
using System.Collections;

public class NextScene : MonoBehaviour {

	public string nextScene;

	public void LoadScene()
	{
		Debug.Log("next scene");
		Application.LoadLevel(nextScene);

	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
