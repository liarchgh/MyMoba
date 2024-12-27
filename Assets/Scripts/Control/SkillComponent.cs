using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[Serializable]
public class SkillComponent
{
	public List<SkillConfig> Skills = new List<SkillConfig>();
	private List<SkillConfig> _runningSkills = new List<SkillConfig>();
	public void CommonUpdate()
	{

		var skillDos = Skills
			.Where(x => x.Trigger.CheckSkillTrigger() && x.Skill.PreCheckLogic())
			.Select(x => (x, x.Skill.GenParam()))
			.ToList();
		skillDos.ForEach(sd =>
		{
			var x = sd.Item1;
			var param = sd.Item2;
			{
				x.Skill.SetParam(param);
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
	[SerializeReference, Subclass]
	public ActionLogicBase Skill;
	[SerializeReference, Subclass]
	public LogicTriggerBase Trigger;
}