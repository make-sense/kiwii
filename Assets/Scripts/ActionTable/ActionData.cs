using UnityEngine;
using System.Collections;

[System.Serializable]
public class ActionParam {
	public string Name;
	public string Value;
}

public enum eActionType {
	Input,
	Output,
}

[System.Serializable]
public class ActionData {
	public string Name;
	public int Guid;
	public Texture texture;
	public eActionType Type;
	public float Length = 0.1f;
	public string CallFunctionName = "";
	public string CallFunctionParam = "";
	public ActionParam[] Params;
}
