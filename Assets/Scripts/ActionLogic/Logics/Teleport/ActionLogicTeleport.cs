using System;
using System.Collections.Generic;
using UnityEngine;
using GameAction;

[Serializable]
public class ActionLogicTeleport: ActionLogicWithParamBase<ActionTeleportParam, ActionTeleportStatus>
{
	public override bool FixedUpdateAction(List<object> value, ActionTeleportStatus actionStatus)
	{
		return true;
	}
	public override void DoActionLogic(List<object> value, ActionCommonData commonData,
		SkillComponent skillComponent, out ActionTeleportStatus actionStatus)
	{
		base.DoActionLogic(value, commonData, skillComponent, out actionStatus);
		var targetPos = ActionParam.GetTargetPosition(value);
		var targetActor = ActionParam.GetTargetActor(value);
		targetActor.transform.position = targetPos;
	}

	public override void Clear(List<object> value, ActionTeleportStatus actionStatus)
	{
		base.Clear(value, actionStatus);
	}

	protected override ActionTeleportStatus CreateStatus()
	{
		return new ActionTeleportStatus();
	}
}