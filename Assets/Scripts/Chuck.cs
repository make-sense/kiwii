using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Threading;

public class Chuck : MonoBehaviour {

	private float CHUCK_WIDTH = 58f;
	private float CHUCK_HEIGHT = 40f;

	public System.Guid Guid;
	private UIRoot _uiRoot = null;
	public Chuck[] _children = new Chuck[1];

	public System.Guid actorGuid;
	public int actionGuid;

	Color inputColor = new Color (1f, 0.5f, 0.5f);
	Color outputColor = new Color (0.5f, 0.5f, 1f);
	Color normalColor = new Color (230f/255f, 180f/255f, 30f/255f);

	public UIButton startButton;

	public bool _isStart = false;

	public void SetAction(System.Guid actorID, int actionID)
	{
		actorGuid = actorID;
		actionGuid = actionID;
		SetActionUI();
	}

	private void SetActionUI() {
		Actor actor = ActorManager.Instance.Get (actorGuid);
		if (actor != null) {
			UIButtonColor baseButtonColor = GetComponentInChildren<UIButtonColor> () as UIButtonColor;
			ActionData actionData = ActionManager.Instance.GetActionData(actionGuid);
			if (actionData != null) {
				if (actionData.Type == eActionType.Input)
					baseButtonColor.defaultColor = inputColor;
				else
					baseButtonColor.defaultColor = outputColor;
			}
			else {
				baseButtonColor.defaultColor = normalColor;
			}

			UIButton baseButton = GetComponentInChildren<UIButton> () as UIButton;
			baseButton.normalSprite = "chuck_base";
			Transform detail = this.transform.FindChild("Detail");
			if (detail != null) {
				detail.gameObject.SetActive(true);
				UIButton button = detail.GetComponentInChildren<UIButton> () as UIButton;
				button.normalSprite = actionData.texture.name;
			}
		}
	}

	void OnDoubleClick () {
		Debug.Log ("Chuck OnDoubleClick:"+Guid.ToString ());
		Execute ();
	}

	public void Execute () {
		// 1. check state

		// 2. run this
		ActionData actionData = ActionManager.Instance.GetActionData(actionGuid);
		if (actionData != null) 
		{
			Actor actor = ActorManager.Instance.Get(actorGuid);
			if (actor != null)
			{
				if (actionData.Type == eActionType.Input) {
					bool resultValue = false;
					MethodInfo methodInfo = actor.GetType().GetMethod(actionData.CallFunctionName);
					object thisvalue = methodInfo.Invoke(actor, null);
					Debug.Log ("Action:" + actionData.CallFunctionName + "=>" + thisvalue.ToString ());
					if (thisvalue.ToString() == "False")
						return;
				}
				else{
					actor.gameObject.BroadcastMessage(actionData.CallFunctionName);
					Debug.Log ("Action:" + actionData.CallFunctionName);
				}
			}
		}

		// 3. run next chuck
		if (_children [0] != null) {
//			Thread thread = new Thread (new ThreadStart (_children [0].Execute));
//			thread.Start ();
			_children [0].Execute ();
		}
	}

	// Use this for initialization
	void Start () {
		Guid = System.Guid.NewGuid ();
		ChuckManager.Instance.Add(this);
		if (UIRoot.list.Count > 0)
			_uiRoot = UIRoot.list[0];
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnPress (bool isPressed) 
	{
		Debug.Log ("Chuck OnPress:"+Guid.ToString ());
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Chuck") 
		{
			Vector3 srcPos = getGlobalPosition(this.transform);
			Vector3 dstPos = getGlobalPosition(other.transform);
			if (isRightEdge(srcPos, dstPos))
			{
				this.transform.parent = other.transform;
				this.transform.localPosition = new Vector3(CHUCK_WIDTH, 0);
				Chuck rootChuck = other.GetComponentInChildren<Chuck> () as Chuck;
				if (rootChuck != null)
					rootChuck._children[1] = this;
			}
			else if (isBottomEdge(srcPos, dstPos))
	        {
				this.transform.parent = other.transform;
				this.transform.localPosition = new Vector3(0, -CHUCK_HEIGHT);
				Chuck rootChuck = other.GetComponentInChildren<Chuck> () as Chuck;
				if (rootChuck != null)
					rootChuck._children[0] = this;
			}
		}
		else if (other.tag == "ChuckStack")
		{
			Debug.Log ("Destroy " + gameObject.name);
			ChuckManager.Instance.Remove(this);
			Destroy(this.gameObject);
		}
	}

	void OnDragEnd () 
	{
		if (isRoot(this.transform))
		    return;

		if (isChuckSeparated(this.transform))
			this.transform.parent = _uiRoot.transform;
	}

	void OnDragDropRelease(GameObject surface)
	{
		Debug.Log ("OnDragDropRelease");
	}

	private bool isRightEdge(Vector3 src, Vector3 dst) 
	{
//		Debug.Log ("isRightEdge");
		if (src.x > dst.x + CHUCK_WIDTH/2 &&
		    src.x < dst.x + CHUCK_WIDTH &&
		    src.y > dst.y - CHUCK_HEIGHT/2 &&
		    src.y < dst.y + CHUCK_HEIGHT)
			return true;
		return false;
	}

	private bool isBottomEdge(Vector3 src, Vector3 dst)
	{
//		Debug.Log ("isBottomEdge");
		if (src.x > dst.x &&
		    src.x < dst.x + CHUCK_WIDTH/2 &&
		    src.y > dst.y - CHUCK_HEIGHT &&
		    src.y < dst.y - CHUCK_HEIGHT/2)
			return true;
		return false;
	}

	private bool isChuckSeparated(Transform src)
	{
		if (src.transform.localPosition.x < CHUCK_WIDTH -CHUCK_WIDTH*0.2f || 
			src.transform.localPosition.x > CHUCK_WIDTH + CHUCK_WIDTH*0.2f ||
		    src.transform.localPosition.y < -CHUCK_HEIGHT - CHUCK_HEIGHT*0.2f ||
		    src.transform.localPosition.y > -CHUCK_HEIGHT + CHUCK_HEIGHT*0.2f)
			return true;
		return false;
	}

	private Vector3 getGlobalPosition(Transform transform)
	{
		if (isRoot (transform))
			return transform.localPosition;
		else
		{
			Vector3 pos = getGlobalPosition(transform.parent);
			if (isRightChild(transform))
				return new Vector3(pos.x+CHUCK_WIDTH, pos.y, 0);
			else
				return new Vector3(pos.x, pos.y-CHUCK_HEIGHT, 0);
		}
	}

	public bool IsRoot()
	{
		return isRoot (transform);
	}

	private bool isRoot(Transform transform)
	{
		if (transform.parent == null)
			return true;

		if (!transform.parent.name.Contains("Chuck"))
			return true;
		return false;
	}

	private bool isRightChild(Transform transform)
	{
		if (isRoot (transform))
			return false;
		if (transform.localPosition.x > CHUCK_WIDTH/2)
			return true;
		return false;
	}

	private bool isBottomChild(Transform transfor)
	{
		if (isRoot (transform))
			return false;
		if (transform.localPosition.y < -CHUCK_HEIGHT/2)
			return true;
		return false;
	}
}
