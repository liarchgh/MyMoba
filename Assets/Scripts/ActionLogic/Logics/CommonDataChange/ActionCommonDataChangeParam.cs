using System;
using System.Collections.Generic;
using GameAction;
using UnityEngine;

[Serializable]
public class ActionCommonDataChangeParam: ActionParamBase
{
	public ActionCommonDataType CommonDataType;
	public float DataValue;
	public float TimeLength;
	public override bool TryGenParam(out List<object> value)
	{
		value = new List<object>(){CommonDataType, DataValue};
		return true;
	}
}