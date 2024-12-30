using System;
using System.Collections.Generic;

[Serializable]
public abstract class ActionLogicWithParamBase<T>: ActionLogicBase where T : ActionParamBase
{
	public T ActionParam;
	public override bool PreCheckLogic(out List<object> value)
	{
		return ActionParam.TryGenParam(out value);
	}
}