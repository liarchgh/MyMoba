using System;

[Serializable]
public abstract class ActionLocigWithParamBase<T>: ActionLogicBase where T : ActionParamBase
{
	public T ActionParam;
	public override ActionParamBase GetParam()
	{
		return ActionParam.GenCopy();
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