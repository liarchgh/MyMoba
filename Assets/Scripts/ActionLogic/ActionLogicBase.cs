using System;

[Serializable]
public abstract class ActionLogicBase
{
	// PreCheckLogic的out参数即为DoLogic的形参
	public abstract bool PreCheckLogic(out object value);
	public abstract void DoLogic(object value);
	public abstract bool FixedUpdate();
	public abstract void Clear();
}
