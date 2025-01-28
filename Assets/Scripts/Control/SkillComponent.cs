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
				if(x.skill.Trigger.CheckSkillTrigger()
						&& x.skill.Skill.PreCheckLogic(out var actionParams))
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
				switch(x.RunType)
				{
					case SkillRunType.OnlyOne:
						_runningSkills
							.Where(x => x.SkillIndex == sd.SkillIndex)
							.ForEach(x => Skills[x.SkillIndex].Skill.Stop(x.Param))
						;
						_runningSkills =
							_runningSkills
							.Where(x => x.SkillIndex != sd.SkillIndex)
							.ToList()
						;
					break;
				}
				var param = sd.Param;
				x.Skill.DoLogic(param);
				// FixedUpdate先跑一次的话虽然可以优化表现，
				// 但是Move跑这里的时候还是上一帧的速度，和上边DoLogic不一样，
				// 会导致再下一帧会向反向走，会认为已经跨过目标点了
				_runningSkills.Add(sd);
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
public enum SkillRunType
{
	OnlyOne = 1,
	Multi = 2,
}
[Serializable]
public class SkillConfig
{
	public string Name;
	public SkillRunType RunType = SkillRunType.Multi;
	[SerializeReference]
	public ActionLogicBase Skill;
	[SerializeReference]
	public LogicTriggerBase Trigger;
}