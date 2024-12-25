using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SkillComponent
{
	public List<SkillConfig> Skills = new List<SkillConfig>();
	private List<SkillConfig> _runningSkills = new List<SkillConfig>();
	public void CommonUpdate()
	{
		Skills.ForEach(x =>
		{
			if(x.Trigger.CheckSkillTrigger())
			{
				x.Skill.DoLogic();
				// 先跑一次，优化表现
				if(!x.Skill.FixedUpdate())
					_runningSkills.Add(x);
			}
		});
	}
	public void CommonFixedUpdate()
	{
		_runningSkills = _runningSkills.Where(x => !x.Skill.FixedUpdate()).ToList();
	}
}
[Serializable]
public class SkillConfig
{
	public SkillShoot Skill;
	public LogicTriggerPressKey Trigger;
}