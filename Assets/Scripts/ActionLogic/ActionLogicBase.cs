using System;
using System.Collections.Generic;

[Serializable]
public abstract class ActionLogicBase
{
	// PreCheckLogic的out参数即为DoLogic的形参
	public abstract bool PreCheckLogic(out List<object> value);
	public abstract void DoLogic(List<object> value, out ActionStatusBase actionStatus);
	public abstract bool FixedUpdate(List<object> value, ActionStatusBase status);
	public abstract void Stop(List<object> value, ActionStatusBase status);
}
