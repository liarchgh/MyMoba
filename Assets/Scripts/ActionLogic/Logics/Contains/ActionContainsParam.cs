using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ActionContainsParam : ActionParamBase
{
	[SerializeReference]
	public ActionParamSingleParamBase<Vector3> PositionParam;
	public override bool TryGenParam(out List<object> value)
	{
		var succ = PositionParam.TryGenValue(out var pos);
		value = new List<object>{pos};
		return succ;
	}
	public Vector3 GetPosition(List<object> value)
	{
		return (Vector3)value.First();
	}
}
