using System;
using Sirenix.OdinInspector;
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
	}
	[SerializeField]
	private ActionLogicType _actionLogicType;
	[SerializeField, ShowIf(nameof(_actionLogicType), ActionLogicType.ActionLogicShoot)]
	private ActionLogicShoot ActionLogicShoot;
	public ActionLogicBase Logic
	{
		get
		{
			switch (_actionLogicType)
			{
				case ActionLogicType.ActionLogicShoot:
					return ActionLogicShoot;
			}
			throw new NotImplementedException();
		}
	}
}
