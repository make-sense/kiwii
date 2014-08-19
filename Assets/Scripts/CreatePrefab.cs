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
		Chuck chuck = instantiatedGameObject.GetComponentInChildren<Chuck> () as Chuck;
		switch (positionOfPrefab.name)
		{
		case "ChuckGo":
			chuck.actionGuid = 100;
			label.text = "->";
			break;
		case "ChuckBack":
			chuck.actionGuid = 200;
			label.text = "<-";
			break;
		case "ChuckJump":
			chuck.actionGuid = 300;
			label.text = "j";
			break;
		case "ChuckSlide":
			chuck.actionGuid = 400;
			label.text = "s";
			break;
		}
	}
}
