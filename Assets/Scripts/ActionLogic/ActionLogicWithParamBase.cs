using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ActionLogicWithParamBase<T>: ActionLogicBase where T : ActionParamBase
{
	public T ActionParam;
	public override bool PreCheckLogic(out List<object> value)
	{
		return ActionParam.TryGenParam(out value);
	}
	public override void DoLogic(List<object> value)
	{
		CacheTime();
	}
	protected void CacheTime()
	{
		ActionParam.DoTime = TimeUtil.GetTime();
	}
}