using UnityEngine;
using System.Collections;

public class ButtonChg : MonoBehaviour {


	public void ButtonImage(GameObject other)
	{
		UIButton button = other.GetComponentInChildren<UIButton> () as UIButton;
	
		button.normalSprite = other.name + "_on";
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
