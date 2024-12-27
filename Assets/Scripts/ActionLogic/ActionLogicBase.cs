using System;

[Serializable]
public abstract class ActionLogicBase
{
	public abstract bool PreCheckLogic();
	public abstract bool FixedUpdate();
	public abstract void DoLogic();
	public abstract void Clear();
}
