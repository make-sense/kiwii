using UnityEngine;
using System.Collections;

public class CreatePrefab : MonoBehaviour {
	public GameObject ChuckStage;
	public GameObject positionOfPrefab;
	public GameObject prefab;
	private static Vector3 _lastPosition;

	void OnDrag (Vector2 delta)
	{
		Transform _transform = ChuckStage.transform.FindChild(positionOfPrefab.name);
		if (_transform != null) {
			_lastPosition = _transform.localPosition;
			Debug.Log(_transform.name + ":" + _lastPosition.ToString());
		}
	}
	
	public void Create()
	{
		GameObject instantiatedGameObject = NGUITools.AddChild(this.transform.parent.gameObject, prefab);
		instantiatedGameObject.transform.localPosition = _lastPosition;
		switch (prefab.name)
		{
		case "Chuck":
			ChuckManager.Instance.Add (instantiatedGameObject.GetComponentInChildren<Chuck> () as Chuck);
			Debug.Log ("Chuck Added!");
			break;
		}
		UILabel label = instantiatedGameObject.GetComponentInChildren<UILabel> () as UILabel;
		switch (positionOfPrefab.name)
		{
		case "ChuckGo":
			label.text = "->";
			break;
		case "ChuckBack":
			label.text = "<-";
			break;
		case "ChuckJump":
			label.text = "j";
			break;
		case "ChuckSlide":
			label.text = "s";
			break;
		}
	}
}
