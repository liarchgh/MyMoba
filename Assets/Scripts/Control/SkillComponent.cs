using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class SkillComponent
{
	// 下标即为ID
	public List<SkillConfig> Skills = new List<SkillConfig>();
	private List<SkillRunData> _runningSkills = new List<SkillRunData>();
	private Queue<SkillRunData> _skillRunDatas = new Queue<SkillRunData>();
	private void CheckSkills()
	{
		var skillsWithParams = Skills
			.Select((skill,index) => (skill, index))
			.Select(x =>
			{
				if(x.skill.Trigger.CheckSkillTrigger() && x.skill.Skill.PreCheckLogic(out var actionParams))
					return (true, x, actionParams);
				else
					return (false, x, null);
			})
			.Where(x => x.Item1)
			.Select(x => new SkillRunData(x.Item2.index, x.Item3));
		AddSkillRunDatas(skillsWithParams);
	}
	public void AddSkillRunDatas(IEnumerable<SkillRunData> skillRunDatas)
	{
		_skillRunDatas.EnqueueRange(skillRunDatas);
	}
	private void HandleSkillLogic()
	{
		_skillRunDatas.ForEach(sd =>
			{
				var x = Skills[sd.SkillIndex];
				var param = sd.Param;
				{
					// TODO: 现在只能放出一个，框架上不应有这种限制
					x.Skill.DoLogic(param);
					// 先跑一次，优化表现
					if(!x.Skill.FixedUpdate(param))
						_runningSkills.Add(sd);
				}
			});
		_skillRunDatas.Clear();
	}
	public void CommonUpdate()
	{
		CheckSkills();
		HandleSkillLogic();
	}
	public void CommonFixedUpdate()
	{
		_runningSkills = _runningSkills
			.Where(x => !Skills[x.SkillIndex].Skill.FixedUpdate(x.Param))
			.ToList();
	}
}
public struct SkillRunData
{
	public int SkillIndex;
	public List<object> Param;
	public SkillRunData(int skillID, List<object> actionParams)
	{
		SkillIndex = skillID;
		Param = actionParams;
	}
}
[Serializable]
public class SkillConfig
{
	public string Name;
	[SerializeReference]
	public ActionLogicBase Skill;
	[SerializeReference]
	public LogicTriggerBase Trigger;
}