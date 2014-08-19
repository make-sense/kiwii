using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorManager : MonoBehaviour {
	BetterList<Actor> _actors = new BetterList<Actor> ();

	// Use this for initialization
	void Start () {
		try 
		{
			GameObject stageGameObject = GameObject.Find ("Stage");
			Stage stage = stageGameObject.GetComponentInChildren<Stage> () as Stage;
			Add (stage);
			Kiwii kiwii = stageGameObject.GetComponentInChildren<Kiwii> () as Kiwii;
			Add (kiwii);
		}
		catch (KeyNotFoundException e) {
			Debug.Log (e.ToString ());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Add (Actor actor) {
		_actors.Add(actor);
	}

	public void Remove (Actor actor) {
		_actors.Remove(actor);
	}

	public Actor Get (System.Guid guid) {
		foreach (Actor actor in _actors) {
			if (actor.Guid == guid)
				return actor;
		}
		return null;
	}

	public Actor Get (Actor.eCharactor type) {
		foreach (Actor actor in _actors) {
			if (actor.charactorType == type)
				return actor;
		}
		return null;
	}

	public int Count () {
		return _actors.size;
	}

	public BetterList<Actor> GetActors() {
		return _actors;
	}

	private static ActorManager _instance = null;
	public static ActorManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(ActorManager)) as ActorManager;
			}
			return _instance;
		}
	}

}
