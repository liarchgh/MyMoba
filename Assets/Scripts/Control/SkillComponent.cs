using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SkillComponent
{
	// 下标即为ID
	public List<SkillConfig> Skills = new List<SkillConfig>();
	private List<SkillConfig> _runningSkills = new List<SkillConfig>();
	private Queue<SkillRunData> _skillRunDatas = new Queue<SkillRunData>();
	private void CheckSkills()
	{
		var skillsWithParams = Skills
            .Select((skill,index) => (skill, index))
			.Where(x => x.skill.Trigger.CheckSkillTrigger() && x.skill.Skill.PreCheckLogic())
			.Select(x => new SkillRunData(x.index, x.skill.Skill.GetParam().GenCopy()));
		_skillRunDatas.EnqueueRange(skillsWithParams);
	}
	private void HandleSkillLogic()
	{
		_skillRunDatas.ForEach(sd =>
			{
				var x = Skills[sd.SkillIndex];
				var param = sd.Param;
				{
					x.Skill.SetParam(param);
					// TODO: 现在只能放出一个，框架上不应有这种限制
					x.Skill.DoLogic();
					// 先跑一次，优化表现
					if(!x.Skill.FixedUpdate())
						_runningSkills.Add(x);
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
		_runningSkills = _runningSkills.Where(x => !x.Skill.FixedUpdate()).ToList();
	}
	private struct SkillRunData
	{
		public int SkillIndex;
		public ActionParamBase Param;
		public SkillRunData(int skillID, ActionParamBase param)
		{
			SkillIndex = skillID;
			Param = param;
		}
	}
}
[Serializable]
public class SkillConfig
{
	public string Name;
	[SerializeReference, Subclass]
	public ActionLogicBase Skill;
	[SerializeReference, Subclass]
	public LogicTriggerBase Trigger;
}