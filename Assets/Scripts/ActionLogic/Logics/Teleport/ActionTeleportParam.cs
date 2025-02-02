using System;
using System.Collections.Generic;
using GameAction;
using UnityEngine;

[Serializable]
public class ActionTeleportParam: ActionParamBase
{
	[SerializeReference]
	private ActionParamSingleParamBase<Vector3> m_targetPosition;
	[SerializeReference]
	private ActionParamSingleParamBase<PlayerControl> m_targetActor;

	public override bool TryGenParam(out List<object> value)
	{
		if(m_targetPosition.TryGenValue(out var v0)
			&& m_targetActor.TryGenValue(out var v1))
		{
			value = new List<object>(){v0, v1};
			return true;
		}
		else
		{
			value = default;
			return false;
		}
	}
	public Vector3 GetTargetPosition(object value)
	{
		return (Vector3)((List<object>)value)[0];
	}
	public PlayerControl GetTargetActor(object value)
	{
		return (PlayerControl)((List<object>)value)[1];
	}
}