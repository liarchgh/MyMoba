using Action;

public class ActionCommonDataChangeStatus: ActionStatusBase
{
	public float DataValue;
	public override float GetDataValue(ActionCommonDataType dataType)
	{
		switch(dataType)
		{
			case ActionCommonDataType.Speed:
				return DataValue;
			default:
				return 0;
		}
	}
	public override bool HaveData(ActionCommonDataType dataType)
	{
		return dataType == ActionCommonDataType.Speed;
	}
}
