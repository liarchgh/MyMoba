using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
				// TODO: 现在只能放出一个，框架上不应有这种限制
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
	[SerializeField]
	private SkillLogicSerialized _skill;
	public SkillLogicBase Skill => _skill.Logic;
	[SerializeField]
	private LogicTriggerSerialized _trigger;
	public LogicTrigger Trigger => _trigger.Trigger;
}