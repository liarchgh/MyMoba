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
	[SerializeReference]
	public ActionParamSingleParamBase<uint> ActionTransform;
	[SerializeReference]
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
public class ActionMoveStatus: ActionStatusBase
{
	public List<ActionLogicGameObjectEntity> _entities = new List<ActionLogicGameObjectEntity>();
	public GameObject Go => _entities[0].GO;
	public bool _moving = false;
	public float dis_last;
}
[Serializable]
public class ActionLogicMove: ActionLogicWithParamBase<ActionMoveParam, ActionMoveStatus>
{
	private List<ActionLogicGameObjectEntity> _entities =
		new List<ActionLogicGameObjectEntity>();
	public float Speed = 1;
	public GameObject ClickRigPrefab;
	public override bool FixedUpdateAction(List<object> value, ActionMoveStatus moveStatus)
	{
		if(!moveStatus._moving) return true;

		var rb = ActionParam.GetActionRigidbody(value);
		var targetPos = ActionParam.GetTargetPosition(value);
		//用和目的地距离变远作为终止条件 但是在碰撞后被弹回会停止
		targetPos.y = rb.position.y;
		var dis_now = Vector3.Distance(targetPos, rb.position);
		if ((dis_now <= moveStatus.dis_last || moveStatus.dis_last < 0) && dis_now > 0.02f) {
			moveStatus.dis_last = dis_now;
		} else {
			StopAction(value, moveStatus);
		}
		return !moveStatus._moving;
	}
	public override void DoActionLogic(List<object> value, out ActionMoveStatus moveStatus)
	{
		base.DoActionLogic(value, out moveStatus);
		var rb = ActionParam.GetActionRigidbody(value);
		var pos = ActionParam.GetTargetPosition(value);
		var rig = GameObject.Instantiate(ClickRigPrefab);
		rig.transform.position = pos;
		moveStatus._entities.Add(new ActionLogicGameObjectEntity(){GO = rig});

		pos.y = rb.position.y;
		rb.linearVelocity = (pos - rb.position).normalized * Speed;
		moveStatus.dis_last = -1;
		moveStatus._moving = true;
	}

	public override void StopAction(List<object> value, ActionMoveStatus moveStatus)
	{
		var rb = ActionParam.GetActionRigidbody(value);
		rb.linearVelocity = Vector3.zero;
		moveStatus._moving = false;
		moveStatus._entities.ForEach(e => e.Clear());
		moveStatus._entities.Clear();
	}

    protected override ActionMoveStatus CreateStatus()
    {
        return new ActionMoveStatus();
    }
}