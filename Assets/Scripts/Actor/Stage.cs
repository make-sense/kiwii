using UnityEngine;
using System.Collections;

public class Stage : Actor {

	public void ToggleRunStop ()
	{
		_isRun = !_isRun;
		SetUI ();
	}

	public void Run ()
	{
		_isRun = true;
		SetUI ();
	}

	public void Stop ()
	{
		_isRun = false;
		SetUI ();
	}

	public bool IsRun ()
	{
		return _isRun;
	}

	void SetUI ()
	{
		UIButton button = GetComponentInChildren<UIButton> () as UIButton;
		if (button == null)
			return;

		if (_isRun) 
			button.normalSprite = "1408105883_traffic_lights_green";
		else 
			button.normalSprite = "1408105883_traffic_lights_red";
	}

	void OnTriggerEnter(Collider other) 
	{
		Debug.Log("Stage::OnTriggerEnter");
		if (other.tag == "Chuck") 
		{
			Debug.Log(other.name);
		}
	}

	bool _isRun = false;

	// Use this for initialization
	void Start () {
		base.Start ();
		base.charactorType = eCharactor.STAGE;
	}
	
	public void Refresh () 
	{
	}

	// Update is called once per frame
	void Update () {

		BetterList<Actor> actors = ActorManager.Instance.GetActors ();
		foreach (Actor actor in actors) 
		{
			switch (actor.charactorType)
			{
//			case Actor.eCharactor.BUTTY:
//				((Butty)actor).Refresh ();
//				break;
//			case Actor.eCharactor.BULBY:
//				((Bulby)actor).Refresh ();
//				break;
			}
		}

		if (IsRun ()) 
		{
			BetterList<Chuck> chucks = ChuckManager.Instance.GetChucks ();
			foreach (Chuck chuck in chucks)
			{
				if (chuck.IsRoot ())
//				if (chuck._isStart)
				{
					chuck.Execute ();
				}
			}
		}
	}
}
