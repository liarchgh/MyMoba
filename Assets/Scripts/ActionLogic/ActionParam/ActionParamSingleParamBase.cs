using System;

[Serializable]
public abstract class ActionParamSingleParamBase<T>
{
	[NonSerialized]
	public T Value;
	public abstract bool TryGenValue();
}
