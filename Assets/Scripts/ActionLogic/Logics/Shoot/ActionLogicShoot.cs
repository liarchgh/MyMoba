using System;
using System.Collections.Generic;
using GameAction;
using UnityEngine;

[Serializable]
public class ActionLogicShoot: ActionLogicWithParamBase<ActionShootParam, ActionShootStatus>
{
	public GameObject Prefab;
	public float Speed = 50;
	private float _lastDis;
	public override bool FixedUpdateAction(List<object> value, ActionShootStatus actionStatus)
	{
		float dis_now = GetDis(value, actionStatus);
		if (dis_now <= actionStatus._lastDis || actionStatus._lastDis < 0) {
			actionStatus._lastDis = dis_now;
			return false;
		} else {
			Clear(value, actionStatus);
			return true;
		}
	}
	public override void DoActionLogic(List<object> value, ActionCommonData commonData,
		SkillComponent skillComponent, out ActionShootStatus actionStatus)
	{
		base.DoActionLogic(value, commonData, skillComponent, out actionStatus);
		var go = GameObject.Instantiate(Prefab);
		var e = new ActionLogicGameObjectEntity(){GO = go};
		actionStatus._entities.Add(e);
		var targetPos = ActionParam.GetEndPosition(value);
		actionStatus._go.transform.position = ActionParam.GetStartPosition(value);
		targetPos.y = actionStatus._go.transform.position.y;
		actionStatus._rb.linearVelocity = (targetPos - actionStatus._go.transform.position).normalized * Speed;
		actionStatus._lastDis = -1;
	}
	public override void Clear(List<object> value, ActionShootStatus actionStatus)
	{
		base.Clear(value, actionStatus);
		actionStatus._entities.ForEach(e => e.Clear());
		actionStatus._entities.Clear();
	}
	private float GetDis(List<object> value, ActionShootStatus shootStatus)
	{
		var targetPos = ActionParam.GetEndPosition(value);
		targetPos.y = shootStatus._go.transform.position.y;
		return Mathf.Abs(Vector3.Distance(targetPos, shootStatus._go.transform.position));
	}

	protected override ActionShootStatus CreateStatus()
	{
		return new ActionShootStatus();
	}
}