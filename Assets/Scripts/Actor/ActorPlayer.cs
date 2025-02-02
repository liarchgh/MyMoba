using UnityEngine;

public class ActorPlayer: ActorBase
{
	public PlayerControl PlayerControl { get; private set; }
	private static string _prefabPath = "Prefabs/Player";
	protected ActorPlayer(): base() { }
	public override void CommonStart()
	{
		var prefab = ResourceMgr.Load<GameObject>(_prefabPath);
		var go = GameObject.Instantiate<GameObject>(prefab);
		PlayerControl = go.GetComponent<PlayerControl>();
		PlayerControl.CommonStart();
	}
	public override void CommonUpdate()
	{
		PlayerControl.CommonUpdate();
	}
	public override void CommonFixedUpdate()
	{
		PlayerControl.CommonFixedUpdate();
	}
	public static uint Create()
	{
		var actor = new ActorPlayer();
		ActorManager.Instance.AddActor(actor);
		return actor.ID;
	}
}
