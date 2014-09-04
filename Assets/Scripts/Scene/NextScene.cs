using UnityEngine;
using System.Collections;

public class NextScene : MonoBehaviour {

	public string _nextScene;
	public float _delay;



	public void LoadScene()
	{
		Invoke("Load", _delay);
		Debug.Log("load scene");
	}

	public void Load()
	{
		Application.LoadLevel(_nextScene);
		Debug.Log("load");
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
