﻿using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public void StartLevel ()
	{
		Debug.Log ("lv1");
		Application.LoadLevel("04_lv1");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
