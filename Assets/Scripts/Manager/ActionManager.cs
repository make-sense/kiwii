using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MsAction {
	public string name;
}

public class ActionManager : MonoBehaviour {

	public Transform actionManager;

	public ActionData GetActionData(int guid)
	{
		foreach (Transform child in actionManager)
		{
			ActionTable actionTable = child.GetComponentInChildren<ActionTable> () as ActionTable;
			if (actionTable != null) {
				foreach (ActionData actionData in actionTable.DataList)
				{
					if (actionData.Guid == guid)
						return actionData;
				}
			}
		}
		return null;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private static ActionManager _instance = null;
	public static ActionManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(ActionManager)) as ActionManager;
			}
			return _instance;
		}
	}
}
