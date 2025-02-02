using UnityEngine;

public class ActorEnemy: ActorBase
{
	private static string _prefabPath = "Prefabs/Enemy";
	public PlayerControl PlayerControl { get; private set; }
	private Vector3 _bornPosition;
	private Quaternion _bornRotation;
	private GameObject _targetGo;
	protected ActorEnemy(): base() { }
	public override void CommonStart()
	{
		var prefab = ResourceMgr.Load<GameObject>(_prefabPath);
		var go = GameObject.Instantiate<GameObject>(prefab, _bornPosition, _bornRotation);

		go.GetComponent<EnemyAI>().player = _targetGo;

		var hpc = go.GetComponent<HPComponent>();
		hpc.OnDie += h =>
		{
			Destroy();
		};

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
	public static uint Create(Vector3 position, Quaternion rotation, GameObject targetGo)
	{
		var actor = new ActorEnemy();
		actor._bornPosition = position;
		actor._bornRotation = rotation;
		actor._targetGo = targetGo;
		ActorManager.Instance.AddActor(actor);
		return actor.ID;
	}
	protected override void Destroy()
	{
		base.Destroy();
		GameObject.Destroy(PlayerControl.gameObject);
		PlayerControl = null;
	}
}
