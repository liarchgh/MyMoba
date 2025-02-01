using System;
using System.Collections.Generic;
using Action;
using UnityEngine;

[Serializable]
public class ActionStopActionParam: ActionParamBase
{
	[SerializeField]
	private ActionLogicType m_targetSkillType;

	public override bool TryGenParam(out List<object> value)
	{
		value = new List<object>(){m_targetSkillType};
		return true;
	}
	public ActionLogicType GetTargetSkillType(object value)
	{
		return (ActionLogicType)((List<object>)value)[0];
	}
}