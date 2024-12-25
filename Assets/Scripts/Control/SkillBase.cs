using System;

[Serializable]
public abstract class SkillBase
{
	public enum TriggerType
	{
		None,
		PlayerControl,
		Auto,
	}
	public abstract TriggerType CheckSkillTrigger();
	public abstract bool FixedUpdate();
	public abstract void DoLogic();
	public abstract void Clear();
}