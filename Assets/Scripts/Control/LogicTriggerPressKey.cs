using System;
using UnityEngine;

[Serializable]
public class LogicTriggerPressKey: LogicTrigger
{
	public KeyCode Key;
    public override bool CheckSkillTrigger()
    {
		return Input.GetKeyDown(Key);
    }
}