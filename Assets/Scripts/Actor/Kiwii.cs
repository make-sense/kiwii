using UnityEngine;
using System.Collections;

public class Kiwii : Actor {
	
	public float _speed = 1.0f;
	private bool isExe = false;
	private string type;
	private float srcPos, dstPos;
	
	public void Go ()
	{
		Debug.Log ("Go");
		//transform.Translate(0.5f, 0, 0);
		srcPos = transform.localPosition.x;
		dstPos = srcPos + 200;
		type = "go";
	}
	
	public void Back ()
	{
		Debug.Log ("Back");
		//transform.Translate(-0.5f, 0, 0);
		srcPos = transform.localPosition.x;
		dstPos = srcPos - 200;
		type = "back";
	}
	
	public void Jump ()
	{
		Debug.Log ("Jump");
		//transform.Translate(0.5f, 0.5f, 0);
		srcPos = transform.localPosition.x;
		dstPos = srcPos + 200;
		type = "jump";
	}
	
	public void Slide ()
	{
		Debug.Log ("Slide");
		//transform.Translate(0.5f, 0, 0);
		srcPos = transform.localPosition.x;
		dstPos = srcPos + 200;
		type = "slide";
	}
	
	public void StartMoving()
	{
		Debug.Log ("Start Moving");
		//isExe = true;
		type = "startmoving";
	}
	
	public override void Refresh ()
	{
	}
	
	/*void OnTriggerEnter(Collider other) 
	{
		Debug.Log ("onTriggerEnter");
		switch (other.tag)
		{
		case "Rule1":
		case "Rule2":
		case "Rule3":

			// Execute Rule1
			Chuck chuck = ChuckManager.Instance.Get (other.tag);
			if (chuck != null){
				Debug.Log("isExe = false");
				isExe = false;
				chuck.Execute ();
			}
			Debug.Log (other.tag);
			break;
		}
	}*/
	
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("OnTriggerEnter2D:" + other.name);
		switch(other.gameObject.tag)
		{
		case "Rule1":
		case "Rule2":
		case "Rule3":
			// Execute Rule1
			Chuck chuck = ChuckManager.Instance.Get (other.gameObject.tag);
			if (chuck != null){
				Debug.Log("isExe = false");
				isExe = false;
				chuck.Execute ();
			}
			Destroy(other.gameObject);
			Debug.Log (other.gameObject.tag);
			break;
		case "Feather":
			Destroy(other.gameObject);
			Debug.Log (other.gameObject.tag);
			break;
		}
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log ("onCollisionEnter");
	}
	
	// Use this for initialization
	void Start () {
		Guid = System.Guid.NewGuid ();
		charactorType = eCharactor.KIWII;
	}
	
	// Update is called once per frame
	void Update () {
		switch (type) {
		case "startmoving":
			if (transform.localPosition.x < 280) 
			{
				transform.Translate (_speed * Time.deltaTime, 0, 0);
			}
			break;
		case "go":
			if (srcPos < dstPos) 
			{
				transform.Translate (_speed * Time.deltaTime, 0, 0);
				srcPos = transform.localPosition.x;	
			}
			break;
		case "back":
			if (srcPos > dstPos) 
			{
				transform.Translate (-(_speed * Time.deltaTime), 0, 0);
				srcPos = transform.localPosition.x;
			}
			break;
		case "jump":
			if (srcPos < dstPos) 
			{
				transform.Translate (_speed * Time.deltaTime, _speed * 3 * Time.deltaTime, 0);
				srcPos = transform.localPosition.x;
			}
			break;
		case "slide":
			if (srcPos < dstPos) 
			{
				transform.Translate (_speed * Time.deltaTime, 0, 0);
				srcPos = transform.localPosition.x;
			}
			break;
		}
	}
}