using System;
using UnityEngine;

[Serializable]
public abstract class ActionLogicBase
{
	public abstract bool FixedUpdate();
	public abstract void DoLogic();
	public abstract void Clear();
}
