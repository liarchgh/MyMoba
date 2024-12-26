using System;
using UnityEngine;

[Serializable]
public abstract class ActionLogicBase
{
	public abstract bool FixedUpdate();
	public abstract void DoLogic();
	public abstract void Clear();
}
public abstract class ActionLogicWithParamBase<T>: ActionLogicBase where T : ActionParam
{
	public ActionParam ActionParam;
}

public class ActionParam
{
}
