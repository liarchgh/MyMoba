using System;

[Serializable]
public abstract class ActionParamWithValueBase<T>: ActionLogicBase where T : ActionParamBase
{
	public T ActionParam;
	public override ActionParamBase GenParam()
	{
		return ActionParam;
	}
	public override void SetParam(ActionParamBase param)
	{
		ActionParam = (T)param;
	}
	public override bool PreCheckLogic()
	{
		return ActionParam.TryGenParam();
	}
}