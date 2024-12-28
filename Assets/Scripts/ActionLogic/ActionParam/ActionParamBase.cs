using System;
using UnityEngine;

[Serializable]
public abstract class ActionParamBase
{
	public virtual string Serialize()
	{
		return $"ls_db {JsonUtility.ToJson(this)}";
	}
	public abstract bool TryGenParam();
	public abstract ActionParamBase GenCopy();
}
