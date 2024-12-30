using System;

[Serializable]
public abstract class ActionParamBase
{
	public abstract bool TryGenParam(out object value);
}
