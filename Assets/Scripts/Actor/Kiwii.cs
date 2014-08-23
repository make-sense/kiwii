using UnityEngine;
using System.Collections;

public class Kiwii : Actor {

	public void Go ()
	{
		Debug.Log ("Go");
	}

	public void Back ()
	{
		Debug.Log ("Back");
	}

	public void Jump ()
	{
		Debug.Log ("Jump");
	}

	public void Slide ()
	{
		Debug.Log ("Slide");
	}

	public override void Refresh ()
	{
	}

	// Use this for initialization
	void Start () {
		Guid = System.Guid.NewGuid ();
		charactorType = eCharactor.KIWII;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
