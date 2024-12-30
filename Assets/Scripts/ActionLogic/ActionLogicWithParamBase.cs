using System;

[Serializable]
public abstract class ActionLogicWithParamBase<T>: ActionLogicBase where T : ActionParamBase
{
	public T ActionParam;
	public override bool PreCheckLogic(out object value)
	{
		return ActionParam.TryGenParam(out value);
	}
}