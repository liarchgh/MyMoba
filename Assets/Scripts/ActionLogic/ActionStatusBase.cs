using System;
using GameAction;

public class ActionStatusBase
{
	public double DoTime;
	public ActionCommonData CommonData;
	public SkillComponent SkillComponent;
	public virtual float GetDataValue(ActionCommonDataType dataType)
	{
		return 0;
	}
	public virtual bool HaveData(ActionCommonDataType dataType)
	{
		return false;
	}
	public ActionLogicType GetStatusType()
	{
		return ActionUtil.GetStatusType(this);
	}
}
