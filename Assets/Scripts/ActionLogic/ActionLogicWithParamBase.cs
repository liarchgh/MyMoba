using System;
using System.Collections.Generic;
using Action;

[Serializable]
public abstract class ActionLogicWithParamBase<T1, T2>: ActionLogicBase
	where T1 : ActionParamBase
	where T2 : ActionStatusBase
{
	public T1 ActionParam;
	public override bool PreCheckLogic(out List<object> value)
	{
		return ActionParam.TryGenParam(out value);
	}
	public override void DoLogic(List<object> value, ActionCommonData commonData,
		SkillComponent skillComponent, out ActionStatusBase status)
	{
		DoActionLogic(value, commonData, skillComponent, out var t2);
		status = t2;
	}
	public virtual void DoActionLogic(List<object> value, ActionCommonData commonData, SkillComponent skillComponent, out T2 actionStatus)
	{
		actionStatus = CreateStatus();
		actionStatus.DoTime = TimeUtil.GetTime();
		actionStatus.CommonData = commonData;
		actionStatus.SkillComponent = skillComponent;
	}
	protected abstract T2 CreateStatus();
	public override bool FixedUpdate(List<object> value, ActionStatusBase status)
	{
		if(status is T2 t2)
		{
			return FixedUpdateAction(value, t2);
		}
		else
		{
			throw new TypeAccessException($"{status.GetType()} is not {typeof(T2)} or its subclass.");
		}
	}
	public virtual bool FixedUpdateAction(List<object> value, T2 actionStatus)
	{
		throw new NotImplementedException();
	}
	public override void Stop(List<object> value, ActionStatusBase status)
	{
		if(status is T2 t2)
		{
			Clear(value, t2);
		}
		else
		{
			throw new TypeAccessException($"{status.GetType()} is not {typeof(T2)} or its subclass.");
		}
	}
	public virtual void Clear(List<object> value, T2 actionStatus)
	{
		actionStatus.CommonData.RemoveData(actionStatus);
	}
}