using System;
using System.Collections.Generic;
using GameAction;
using UnityEngine;

[Serializable]
public class ActionMoveParam: ActionParamBase
{
	[SerializeReference]
	private ActionParamSingleParamBase<uint> ActionTransform;
	[SerializeReference]
	private ActionParamSingleParamBase<Vector3> TargetPosition;
	public ActionCommonDataType SpeedMultiFrom;

	public override bool TryGenParam(out List<object> value)
	{
		if(ActionTransform.TryGenValue(out var v0)
			&& TargetPosition.TryGenValue(out var v1))
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
	public Rigidbody GetActionRigidbody(object value)
	{
		var actor = ActorManager.Instance.GetActor((uint)((List<object>)value)[0]) as ActorPlayer;
		return actor.PlayerControl.GetComponent<Rigidbody>();
	}
	public Vector3 GetTargetPosition(object value)
	{
		return (Vector3)((List<object>)value)[1];
	}
}