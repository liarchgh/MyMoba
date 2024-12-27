using System;

[Serializable]
public abstract class ActionLogicBase
{
	public abstract bool PreCheckLogic();
	public abstract ActionParamBase GenParam();
	public abstract void SetParam(ActionParamBase param);
	public abstract bool FixedUpdate();
	public abstract void DoLogic();
	public abstract void Clear();
}
