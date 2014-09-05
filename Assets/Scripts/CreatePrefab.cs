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
			/*if(this.tag == "Chuck")
			{
				transform.localScale = new Vector3(0.5f,0.5f,0);
			}*/

//			Debug.Log(_transform.name + ":" + _lastPosition.ToString());
		}
	}
	
	public void Create()
	{
		GameObject instantiatedGameObject = NGUITools.AddChild(this.transform.parent.gameObject, prefab);
		instantiatedGameObject.transform.localPosition = _lastPosition;
		UIButton button = instantiatedGameObject.GetComponentInChildren<UIButton> () as UIButton;
		Chuck chuck = instantiatedGameObject.GetComponentInChildren<Chuck> () as Chuck;
		switch (positionOfPrefab.name)
		{
		case "ChuckGo":
			chuck.actionGuid = 100;
			button.normalSprite = "kiwii_chuck_go";
			break;
		case "ChuckBack":
			chuck.actionGuid = 200;
			button.normalSprite = "kiwii_chuck_back";
			break;
		case "ChuckJump":
			chuck.actionGuid = 300;
			button.normalSprite = "kiwii_chuck_jump";
			break;
		case "ChuckSlide":
			chuck.actionGuid = 400;
			button.normalSprite = "kiwii_chuck_slide";
			break;
		}
	}
}
