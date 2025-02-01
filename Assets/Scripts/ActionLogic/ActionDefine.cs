using System;

namespace Action
{
	public enum ActionLogicType
	{
		None=0,
		CommonDataChange=1,
		Contains=2,
		Die=3,
		Move=4,
		Shoot=5,
		StopAction=6,
	}
	public static class ActionUtil
	{
		public static ActionLogicType GetLogicType(ActionLogicBase actionLogic)
		{
			switch (actionLogic)
			{
				case ActionLogicCommonDataChange:
					return ActionLogicType.CommonDataChange;
				case ActionLogicContains:
					return ActionLogicType.Contains;
				case ActionLogicDie:
					return ActionLogicType.Die;
				case ActionLogicMove:
					return ActionLogicType.Move;
				case ActionLogicShoot:
					return ActionLogicType.Shoot;
				case ActionLogicStopAction:
					return ActionLogicType.StopAction;
				default:
					throw new ArgumentOutOfRangeException($"type:{actionLogic.GetType()} has no logic type");
			}
		}
		public static ActionLogicType GetParamType(ActionParamBase actionParam)
		{
			switch (actionParam)
			{
				case ActionCommonDataChangeParam:
					return ActionLogicType.CommonDataChange;
				case ActionContainsParam:
					return ActionLogicType.Contains;
				case ActionDieParam:
					return ActionLogicType.Die;
				case ActionMoveParam:
					return ActionLogicType.Move;
				case ActionShootParam:
					return ActionLogicType.Shoot;
				case ActionStopActionParam:
					return ActionLogicType.StopAction;
				default:
					throw new ArgumentOutOfRangeException($"type:{actionParam.GetType()} has no logic type");
			}
		}
		public static ActionLogicType GetStatusType(ActionStatusBase actionParam)
		{
			switch (actionParam)
			{
				case ActionCommonDataChangeStatus:
					return ActionLogicType.CommonDataChange;
				case ActionContainsStatus:
					return ActionLogicType.Contains;
				case ActionDieStatus:
					return ActionLogicType.Die;
				case ActionMoveStatus:
					return ActionLogicType.Move;
				case ActionShootStatus:
					return ActionLogicType.Shoot;
				case ActionStopActionStatus:
					return ActionLogicType.StopAction;
				default:
					throw new ArgumentOutOfRangeException($"type:{actionParam.GetType()} has no logic type");
			}
		}
	}
}