using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionDieParam: ActionParamBase
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
