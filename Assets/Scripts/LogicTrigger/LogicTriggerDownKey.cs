using System;
using UnityEngine;

[Serializable]
public class LogicTriggerDownKey: LogicTriggerBase
{
	public KeyCode Key;
	public override bool CheckSkillTrigger()
	{
		return Input.GetKeyDown(Key);
	}
}