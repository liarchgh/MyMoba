using System;
using System.Collections.Generic;
using GameAction;

[Serializable]
public abstract class ActionParamBase
{
	public abstract bool TryGenParam(out List<object> value);
	public ActionLogicType GetParamType()
	{
		return ActionUtil.GetParamType(this);
	}
}
