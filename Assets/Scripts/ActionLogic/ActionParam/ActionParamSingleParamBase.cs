using System;

[Serializable]
public abstract class ActionParamSingleParamBase<T>
{
	public abstract bool TryGenValue(out T value);
}
