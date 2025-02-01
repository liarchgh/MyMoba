using System;
using System.Collections.Generic;
using UnityEngine;
using Action;

[Serializable]
public class ActionLogicStopAction: ActionLogicWithParamBase<ActionStopActionParam, ActionStopActionStatus>
{
	public override bool FixedUpdateAction(List<object> value, ActionStopActionStatus actionStatus)
	{
		return true;
	}
	public override void DoActionLogic(List<object> value, ActionCommonData commonData,
		SkillComponent skillComponent, out ActionStopActionStatus actionStatus)
	{
		base.DoActionLogic(value, commonData, skillComponent, out actionStatus);
		var targetSkillType = ActionParam.GetTargetSkillType(value);
		actionStatus.SkillComponent.StopActionsBy(x=>x.ActionStatus.GetStatusType() == targetSkillType);
	}

	public override void Clear(List<object> value, ActionStopActionStatus actionStatus)
	{
		base.Clear(value, actionStatus);
	}

	protected override ActionStopActionStatus CreateStatus()
	{
		return new ActionStopActionStatus();
	}
}