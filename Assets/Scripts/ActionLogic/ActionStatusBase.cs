using System;
using Action;

public class ActionStatusBase
{
	public double DoTime;
	public ActionCommonData CommonData;
	public virtual float GetDataValue(ActionCommonDataType dataType)
	{
		return 0;
	}
	public virtual bool HaveData(ActionCommonDataType dataType)
	{
		return false;
	}
}
