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
[Serializable]
public class ActionLogicShoot: ActionLogicWithParamBase<ActionShootParam>
{
	public GameObject Prefab;
	public float Speed = 50;
	private List<ActionLogicGameObjectEntity> _entities = new List<ActionLogicGameObjectEntity>();
	private GameObject _go => _entities[0].GO;
	private Rigidbody _rb => _go.GetComponent<Rigidbody>();
	private float _lastDis;
	public override bool FixedUpdate(List<object> value)
	{
		float dis_now = GetDis(value);
		if (dis_now <= _lastDis || _lastDis < 0) {
			_lastDis = dis_now;
			return false;
		} else {
			Stop(value);
			return true;
		}
	}
	public override void DoLogic(List<object> value)
	{
		base.DoLogic(value);
		var go = GameObject.Instantiate(Prefab);
		var e = new ActionLogicGameObjectEntity(){GO = go};
		_entities.Add(e);
		var targetPos = ActionParam.GetEndPosition(value);
		_go.transform.position = ActionParam.GetStartPosition(value);
		targetPos.y = _go.transform.position.y;
		_rb.linearVelocity = (targetPos - _go.transform.position).normalized * Speed;
		_lastDis = -1;
	}
	public override void Stop(List<object> value)
	{
		_entities.ForEach(e => e.Clear());
		_entities.Clear();
	}
	private float GetDis(List<object> value)
	{
		var targetPos = ActionParam.GetEndPosition(value);
		targetPos.y = _go.transform.position.y;
		return Mathf.Abs(Vector3.Distance(targetPos, _go.transform.position));
	}
}