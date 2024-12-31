using System;
using System.Collections.Generic;
using UnityEngine;


public class ActionMoveEntity : EntityBase
{
	public GameObject GO;
	public override void Clear()
	{
		GameObject.Destroy(GO);
	}
}

[Serializable]
public class ActionMoveParam: ActionParamBase
{
	[SerializeReference, Subclass]
	public ActionParamSingleParamBase<uint> ActionTransform;
	[SerializeReference, Subclass]
	public ActionParamSingleParamBase<Vector3> TargetPosition;

	public override bool TryGenParam(out List<object> value)
	{
		if(ActionTransform.TryGenValue(out var v0)
			&& TargetPosition.TryGenValue(out var v1))
		{
			value = new List<object>(){v0, v1};
			return true;
		}
		else
		{
			value = default;
			return false;
		}
	}
	public Rigidbody GetActionRigidbody(object value)
	{
		return PlayerManager.Instance.GetPlayerControl((uint)((List<object>)value)[0]).GetComponent<Rigidbody>();
	}
	public Vector3 GetTargetPosition(object value)
	{
		return (Vector3)((List<object>)value)[1];
	}
}
[Serializable]
public class ActionLogicMove: ActionLogicWithParamBase<ActionMoveParam>
{
	private List<ActionLogicGameObjectEntity> _entities =
		new List<ActionLogicGameObjectEntity>();
	public float Speed = 1;
	public GameObject ClickRigPrefab;
	private bool _moving = false;
	private float dis_last;
	public override bool FixedUpdate(List<object> value)
	{
		if(!_moving) return true;

		var rb = ActionParam.GetActionRigidbody(value);
		var pos = ActionParam.GetTargetPosition(value);
		//用和目的地距离变远作为终止条件 但是在碰撞后被弹回会停止
		pos.y = rb.position.y;
		float dis_now = Mathf.Abs(Vector3.Distance(pos, rb.position));
		if ((dis_now <= dis_last || dis_last < 0) && dis_now > 0.2f) {
			dis_last = dis_now;
		} else {
			rb.linearVelocity = Vector3.zero;
			Clear();
			_moving = false;
		}
		return !_moving;
	}
	public override void DoLogic(List<object> value)
	{
		var rb = ActionParam.GetActionRigidbody(value);
		var pos = ActionParam.GetTargetPosition(value);
		rb.linearVelocity = (pos - rb.position).normalized * Speed;
		var rig = GameObject.Instantiate(ClickRigPrefab);
		rig.transform.position = pos;
		_entities.Add(new ActionLogicGameObjectEntity(){GO = rig});
		dis_last = -1;
		_moving = true;
	}

	public override void Clear()
	{
		_entities.ForEach(e => e.Clear());
		_entities.Clear();
	}
}