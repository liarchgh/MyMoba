using System;
using UnityEngine;

[Serializable]
public class LogicTriggerAlways: LogicTriggerBase
{
	public override bool CheckSkillTrigger()
	{
		return true;
	}
}