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
