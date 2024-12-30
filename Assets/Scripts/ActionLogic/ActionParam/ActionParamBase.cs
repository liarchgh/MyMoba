using System;
using System.Collections.Generic;

[Serializable]
public abstract class ActionParamBase
{
	public abstract bool TryGenParam(out List<object> value);
}
