using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public abstract class SkillLogicBase
{
	public abstract bool FixedUpdate();
	public abstract void DoLogic();
	public abstract void Clear();
}
[Serializable]
public class SkillLogicSerialized
{
	[Serializable]
	public enum SkillLogicType
	{
		SkillShoot,
	}
	[SerializeField]
	private SkillLogicType _skillLogicType;
	[SerializeField, ShowIf(nameof(_skillLogicType), SkillLogicType.SkillShoot)]
	private SkillShoot SkillShoot;
	public SkillLogicBase Logic
	{
		get
		{
			switch (_skillLogicType)
			{
				case SkillLogicType.SkillShoot:
					return SkillShoot;
			}
			throw new NotImplementedException();
		}
	}
}
