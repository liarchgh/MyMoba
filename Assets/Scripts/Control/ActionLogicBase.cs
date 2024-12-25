using System;
using UnityEngine;

[Serializable]
public abstract class ActionLogicBase
{
	public abstract bool FixedUpdate();
	public abstract void DoLogic();
	public abstract void Clear();
}
[Serializable]
public class ActionLogicSerialized
{
	[Serializable]
	public enum ActionLogicType
	{
		ActionLogicShoot,
		ActionLogicContains,
	}
	[SerializeField]
	private ActionLogicType _actionLogicType;
	[SerializeField]
	private ActionLogicShoot ActionLogicShoot;
	[SerializeField]
	private ActionLogicContains ActionLogicContains;

	public ActionLogicBase Logic
	{
		get
		{
			switch (_actionLogicType)
			{
				case ActionLogicType.ActionLogicShoot:
					return ActionLogicShoot;
				case ActionLogicType.ActionLogicContains:
					return ActionLogicContains;
			}
			throw new NotImplementedException();
		}
	}
}
