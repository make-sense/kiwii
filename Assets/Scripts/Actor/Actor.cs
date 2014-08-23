using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {
	public string DestroyTag;

	public enum eCharactor {
		NONE,
		STAGE,
		KIWII,
	};
	public eCharactor charactorType = eCharactor.NONE;

	public System.Guid Guid;
	public string ActorName;
	public Vector3 Pos;

	void Start ()
	{
	}

	public virtual void Refresh ()
	{
		Debug.Log ("Actor::Refresh");
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == DestroyTag)
		{
			Debug.Log ("Destroy " + gameObject.name);
			Destroy(this.gameObject);
		}
	}
}
