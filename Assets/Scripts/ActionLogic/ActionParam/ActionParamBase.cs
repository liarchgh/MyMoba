using System;
using System.Collections.Generic;

[Serializable]
public abstract class ActionParamBase
{
	[NonSerialized]
	public double DoTime;
	public abstract bool TryGenParam(out List<object> value);
}
