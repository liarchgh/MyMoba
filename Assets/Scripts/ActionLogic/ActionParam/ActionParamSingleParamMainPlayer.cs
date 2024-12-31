using System;
using TreeEditor;
using UnityEngine;

[Serializable]
public class ActionParamSingleParamMainPlayer : ActionParamSingleParamBase<uint>
{
	public override bool TryGenValue(out uint pid)
	{
		pid = BattleManger.Instance.MainPlayerID;
		return true;
	}
}
