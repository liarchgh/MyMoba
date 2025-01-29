using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionShootParam: ActionParamBase
{
	[SerializeReference]
	public ActionParamSingleParamBase<Vector3> StartPositionGen;
	[SerializeReference]
	public ActionParamSingleParamBase<Vector3> EndPositionGen;

	public override bool TryGenParam(out List<object> value)
	{
		if(StartPositionGen.TryGenValue(out var v0)
			&& EndPositionGen.TryGenValue(out var v1))
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
	public Vector3 GetStartPosition(object value)
	{
		return (Vector3)((List<object>)value)[0];
	}
	public Vector3 GetEndPosition(object value)
	{
		return (Vector3)((List<object>)value)[1];
	}
}
public class ActionShootStatus: ActionStatusBase
{
	public List<ActionLogicGameObjectEntity> _entities = new List<ActionLogicGameObjectEntity>();
	public GameObject _go => _entities[0].GO;
	public Rigidbody _rb => _go.GetComponent<Rigidbody>();
	public float _lastDis;
}
[Serializable]
public class ActionLogicShoot: ActionLogicWithParamBase<ActionShootParam, ActionShootStatus>
{
	public GameObject Prefab;
	public float Speed = 50;
	private float _lastDis;
	public override bool FixedUpdateAction(List<object> value, ActionShootStatus shootStatus)
	{
		float dis_now = GetDis(value, shootStatus);
		if (dis_now <= shootStatus._lastDis || shootStatus._lastDis < 0) {
			shootStatus._lastDis = dis_now;
			return false;
		} else {
			StopAction(value, shootStatus);
			return true;
		}
	}
	public override void DoActionLogic(List<object> value, out ActionShootStatus shootStatus)
	{
		base.DoActionLogic(value, out shootStatus);
		var go = GameObject.Instantiate(Prefab);
		var e = new ActionLogicGameObjectEntity(){GO = go};
		shootStatus._entities.Add(e);
		var targetPos = ActionParam.GetEndPosition(value);
		shootStatus._go.transform.position = ActionParam.GetStartPosition(value);
		targetPos.y = shootStatus._go.transform.position.y;
		shootStatus._rb.linearVelocity = (targetPos - shootStatus._go.transform.position).normalized * Speed;
		shootStatus._lastDis = -1;
	}
	public override void StopAction(List<object> value, ActionShootStatus shootStatus)
	{
		shootStatus._entities.ForEach(e => e.Clear());
		shootStatus._entities.Clear();
	}
	private float GetDis(List<object> value, ActionShootStatus shootStatus)
	{
		var targetPos = ActionParam.GetEndPosition(value);
		targetPos.y = shootStatus._go.transform.position.y;
		return Mathf.Abs(Vector3.Distance(targetPos, shootStatus._go.transform.position));
	}

    protected override ActionShootStatus CreateStatus()
    {
		return new ActionShootStatus();
    }
}