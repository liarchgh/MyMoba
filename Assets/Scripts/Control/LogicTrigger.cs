using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public abstract class LogicTrigger
{
	public abstract bool CheckSkillTrigger();
}

[Serializable]
public class LogicTriggerSerialized
{
	[Serializable]
	public enum LogicTriggerType
	{
		LogicTriggerPressKey,
	}
	[SerializeField]
	private LogicTriggerType _logicTriggerType;
	[SerializeField, ShowIf(nameof(_logicTriggerType), LogicTriggerType.LogicTriggerPressKey)]
	private LogicTriggerPressKey LogicTriggerPressKey;
	public LogicTrigger Trigger
	{
		get
		{
			switch (_logicTriggerType)
			{
				case LogicTriggerType.LogicTriggerPressKey:
					return LogicTriggerPressKey;
			}
			throw new NotImplementedException();
		}
	}
}