using System;
using System.Collections.Generic;
using UnityEngine;
using GameAction;

[Serializable]
public class ActionLogicDie: ActionLogicWithParamBase<ActionDieParam, ActionDieStatus>
{
	public float Speed = 1;
	public GameObject ClickRigPrefab;
	public override bool FixedUpdateAction(List<object> value, ActionDieStatus actionStatus)
	{
		if(!actionStatus._moving) return true;

		var rb = ActionParam.GetActionRigidbody(value);
		var pos = ActionParam.GetTargetPosition(value);
		//用和目的地距离变远作为终止条件 但是在碰撞后被弹回会停止
		pos.y = rb.position.y;
		float dis_now = Mathf.Abs(Vector3.Distance(pos, rb.position));
		if ((dis_now <= actionStatus.dis_last || actionStatus.dis_last < 0) && dis_now > 0.2f) {
			actionStatus.dis_last = dis_now;
		} else {
			rb.linearVelocity = Vector3.zero;
			Clear(value, actionStatus);
			actionStatus._moving = false;
		}
		return !actionStatus._moving;
	}
	public override void DoActionLogic(List<object> value, ActionCommonData commonData,
		SkillComponent skillComponent, out ActionDieStatus actionStatus)
	{
		base.DoActionLogic(value, commonData, skillComponent, out actionStatus);
		var rb = ActionParam.GetActionRigidbody(value);
		var pos = ActionParam.GetTargetPosition(value);
		var rig = GameObject.Instantiate(ClickRigPrefab);
		rig.transform.position = pos;
		actionStatus._entities.Add(new ActionLogicGameObjectEntity(){GO = rig});

		pos.y = rb.position.y;
		rb.linearVelocity = (pos - rb.position).normalized * Speed;
		actionStatus.dis_last = -1;
		actionStatus._moving = true;
	}

	public override void Clear(List<object> value, ActionDieStatus actionStatus)
	{
		base.Clear(value, actionStatus);
		actionStatus._entities.ForEach(e => e.Clear());
		actionStatus._entities.Clear();
	}

	protected override ActionDieStatus CreateStatus()
	{
		return new ActionDieStatus();
	}
}