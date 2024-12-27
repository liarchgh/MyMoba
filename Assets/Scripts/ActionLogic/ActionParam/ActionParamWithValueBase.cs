using System;

[Serializable]
public abstract class ActionParamWithValueBase<T>: ActionLogicBase where T : ActionParamBase
{
	public T ActionParam;
	public override bool PreCheckLogic()
	{
		return ActionParam.TryGenParam();
	}
}