using System;
using UnityEngine;

[Serializable]
public class ActionParamSingleParamPlayerController : ActionParamSingleParamBase<PlayerControl>
{
	public override bool TryGenValue(out PlayerControl actor)
	{
		actor = BattleManger.Instance.MainPlayer;
		return true;
	}
}
