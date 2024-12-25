using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public abstract class LogicTriggerBase
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
	[SerializeField, ShowIf("@this._logicTriggerType == LogicTriggerType.LogicTriggerPressKey")]
	private LogicTriggerPressKey LogicTriggerPressKey;
	public LogicTriggerBase Trigger
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