using UnityEngine;
using System.Collections;

public class Kiwii : Actor {
	
	public float _speed = 1.0f;
	private string type;
	private bool direction = true;
	private float srcPos, dstPos;
	private GameObject mountain, sky;
	private GameObject startBtn, stopBtn;
	
	public void Go ()
	{
		Debug.Log ("Go");
		//srcPos = transform.localPosition.x;
		//dstPos = srcPos + 200;
		type = "go";
		direction = true;
		transform.localRotation = Quaternion.Euler(new Vector3 (0, 0, 0));
	}
	
	public void Back ()
	{
		Debug.Log ("Back");
		//srcPos = transform.localPosition.x;
		//dstPos = srcPos - 200;
		type = "back";
		direction = false;
		transform.localRotation = Quaternion.Euler(new Vector3 (180.0f, 0, 180.0f));
	}
	
	public void Jump ()
	{
		Debug.Log ("Jump");
		srcPos = transform.localPosition.x;
		if (direction) 
		{
			dstPos = srcPos + 150;
		} 
		else if (!direction) 
		{
			dstPos = srcPos - 150;
		}
		type = "jump";
	}
	
	public void Slide ()
	{
		Debug.Log ("Slide");
		srcPos = transform.localPosition.x;
		if (direction) 
		{
			dstPos = srcPos + 150;
			transform.localRotation = Quaternion.Euler(new Vector3 (0, 0, 90.0f));
		} 
		else if (!direction) 
		{
			dstPos = srcPos - 150;
			transform.localRotation = Quaternion.Euler(new Vector3 (0, 180.0f, 90.0f));
		}
		type = "slide";

	}
	
	public void StartMoving()
	{
		Debug.Log ("Start Moving");
		type = "startmoving";
		startBtn.SetActive(false);
		stopBtn.SetActive (true);
	}

	public void StopMoving()
	{
		Debug.Log("Stop Moving");
		type = "stop";
		startBtn.SetActive(true);
		stopBtn.SetActive (false);
	}

	public override void Refresh ()
	{
	}

	public void ToInitPos ()
	{
		transform.localPosition = new Vector3 (-522, 94);
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
				chuck.Execute ();
			}
			Destroy(other.gameObject);
			Debug.Log ("other.gameObject.tag : " + other.gameObject.tag);
			break;
		case "Feather":
			Destroy(other.gameObject);
			Debug.Log (other.gameObject.tag);
			Application.LoadLevel("lv2");
			break;
		}
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log ("onCollisionEnter");
	}

	void BackgroundMove()
	{
		mountain.transform.Translate(-0.05f * Time.deltaTime, 0, 0);
		if(mountain.transform.localPosition.x < -180)
		{
			mountain.transform.localPosition = new Vector3(-145, 131, 0);
		}
		sky.transform.Translate(-0.1f * Time.deltaTime, 0, 0);
		if(sky.transform.localPosition.x < -180)
		{
			sky.transform.localPosition = new Vector3(-145, 272, 0);
		}
	}
	
	// Use this for initialization
	void Start () {
		Guid = System.Guid.NewGuid ();
		charactorType = eCharactor.KIWII;

		mountain = GameObject.Find ("Mountain");
		sky = GameObject.Find ("Sky");
		startBtn = GameObject.Find ("Play");
		stopBtn = GameObject.Find ("Pause");
	}
	
	// Update is called once per frame
	void Update () {
		switch (type) {
		case "startmoving":
			if (transform.localPosition.x < 280 && transform.localPosition.x > -580) 
			{
				transform.Translate (_speed * Time.deltaTime, 0, 0);
				BackgroundMove();
			}
			break;
		case "stoptmoving":
			transform.Translate (0, 0, 0);
			break;
		case "go":
			if (transform.localPosition.x < 280) 
			{
				transform.Translate (_speed * Time.deltaTime, 0, 0);
				srcPos = transform.localPosition.x;

				BackgroundMove();
			}
			break;
		case "back":
			if (transform.localPosition.x > -580) 
			{
				transform.Translate (_speed * Time.deltaTime, 0, 0);
				//srcPos = transform.localPosition.x;
				BackgroundMove();
			}
			break;
		case "jump":
			if (direction) 
			{
				if(srcPos < dstPos)
				{
					transform.Translate (_speed * 2 * Time.deltaTime, _speed * 5 * Time.deltaTime, 0);
					srcPos = transform.localPosition.x;
					BackgroundMove();
				}
				else
				{
					type = "go";
				}
			}
			else if(!direction)
			{
				if(srcPos > dstPos)
				{
					transform.Translate (_speed * 2 * Time.deltaTime, _speed * 5 * Time.deltaTime, 0);
					srcPos = transform.localPosition.x;
					BackgroundMove();
				}
				else
				{
					type = "back";
				}
			}
			break;
		case "slide":
			if (direction) 
			{
				if(srcPos < dstPos)
				{
					transform.Translate (0, -(_speed * Time.deltaTime), 0);
					srcPos = transform.localPosition.x;
					BackgroundMove();
				}
				else
				{
					Go ();
				}
			}
			else if(!direction)
			{
				if(srcPos > dstPos)
				{
					transform.Translate (0, -(_speed * Time.deltaTime), 0);
					srcPos = transform.localPosition.x;
					BackgroundMove();
				}
				else
				{
					Back();
				}
			}
			break;
		}
	}
}