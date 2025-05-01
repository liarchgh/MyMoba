using System.Collections.Generic;
using UnityEngine;

public delegate Vector3 GetTargetPosition();
public delegate bool IsEnd<UpdateData>(bool start, ref UpdateData updateData);
public class ActorBullet<UpdateData>: ActorBase
{
	private UpdateData _updateData;
	public GameObject _go = null;
	private GetTargetPosition _getTargetPositionDel;
	private IsEnd<UpdateData> _isEndDel;
	private Vector3 _nowTarget = Vector3.zero;
	private bool IsTargetPositionChange(bool forceChange, out Vector3 targetPosition)
	{
		targetPosition = _getTargetPositionDel();
		return forceChange || _nowTarget != targetPosition;
	}
	private void SetTarget(Vector3 target)
	{
		_nowTarget = target;
		_go.transform.GetComponent<Rigidbody>().linearVelocity = _nowTarget;
	}
	public override void CommonStart()
	{
		Update(true);
	}
	public override void CommonFixedUpdate()
	{
		Update(false);
	}
	private void Update(bool start)
	{
		if(_isEndDel(start, ref _updateData))
		{
			Destroy();
			return;
		}
		if(IsTargetPositionChange(start, out var targetPosition))
		{
			SetTarget(targetPosition);
		}
	}
	protected override void Destroy()
	{
		base.Destroy();
		GameObject.Destroy(_go);
	}
	private ActorBullet(GameObject gos, GetTargetPosition getTargetPosition, IsEnd<UpdateData> isEnd)
	{
		_go = gos;
		_getTargetPositionDel = getTargetPosition;
		_isEndDel = isEnd;
	}
	public static ActorBullet<UpdateData> Create(
		GameObject gos,
		GetTargetPosition getTargetPosition,
		IsEnd<UpdateData> isEnd
		)
	{
		var ab = new ActorBullet<UpdateData>(gos, getTargetPosition, isEnd);
		ActorManager.Instance.AddActor(ab);
		return ab;
	}
}
