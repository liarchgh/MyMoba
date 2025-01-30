using System;
using System.Collections.Generic;
using UnityEngine;
using Action;

[Serializable]
public class ActionLogicCommonDataChange: ActionLogicWithParamBase<
	ActionCommonDataChangeParam, ActionCommonDataChangeStatus>
{
	public override bool FixedUpdateAction(
		List<object> value, ActionCommonDataChangeStatus actionStatus)
	{
		return TimeUtil.GetTime() > actionStatus.DoTime + ActionParam.TimeLength;
	}
	public override void DoActionLogic(List<object> value,
		ActionCommonData commonData, out ActionCommonDataChangeStatus actionStatus)
	{
		base.DoActionLogic(value, commonData, out actionStatus);
		actionStatus.DataValue = ActionParam.DataValue;
		commonData.AddData(ActionParam.CommonDataType, actionStatus);
	}

	public override void Clear(List<object> value, ActionCommonDataChangeStatus actionStatus)
	{
		base.Clear(value, actionStatus);
	}

	protected override ActionCommonDataChangeStatus CreateStatus()
	{
		return new ActionCommonDataChangeStatus();
	}
}