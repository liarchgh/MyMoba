using System;
using System.Collections.Generic;
using UnityEngine;
using Action;

[Serializable]
public class ActionLogicMove: ActionLogicWithParamBase<ActionMoveParam, ActionMoveStatus>
{
	public float BaseSpeed = 1;
	public GameObject ClickRigPrefab;
	public override bool FixedUpdateAction(List<object> value, ActionMoveStatus actionStatus)
	{
		if(!actionStatus._moving) return true;

		var rb = ActionParam.GetActionRigidbody(value);
		var targetPos = ActionParam.GetTargetPosition(value);
		//用和目的地距离变远作为终止条件 但是在碰撞后被弹回会停止
		targetPos.y = rb.position.y;
		var dis_now = Vector3.Distance(targetPos, rb.position);
		if ((dis_now <= actionStatus.dis_last || actionStatus.dis_last < 0) && dis_now > 0.02f) {
			actionStatus.dis_last = dis_now;
		} else {
			Clear(value, actionStatus);
		}
		return !actionStatus._moving;
	}
	public override void DoActionLogic(List<object> value, ActionCommonData commonData,
		SkillComponent skillComponent, out ActionMoveStatus actionStatus)
	{
		base.DoActionLogic(value, commonData, skillComponent, out actionStatus);
		var rb = ActionParam.GetActionRigidbody(value);
		var pos = ActionParam.GetTargetPosition(value);
		var rig = GameObject.Instantiate(ClickRigPrefab);
		rig.transform.position = pos;
		actionStatus._entities.Add(new ActionLogicGameObjectEntity(){GO = rig});

		pos.y = rb.position.y;
		rb.linearVelocity = (pos - rb.position).normalized * GetSpeed(actionStatus);
		actionStatus.dis_last = -1;
		actionStatus._moving = true;
	}

	public override void Clear(List<object> value, ActionMoveStatus actionStatus)
	{
		base.Clear(value, actionStatus);
		var rb = ActionParam.GetActionRigidbody(value);
		rb.linearVelocity = Vector3.zero;
		actionStatus._moving = false;
		actionStatus._entities.ForEach(e => e.Clear());
		actionStatus._entities.Clear();
	}

	protected override ActionMoveStatus CreateStatus()
	{
		return new ActionMoveStatus();
	}
	private float GetSpeed(ActionMoveStatus actionStatus)
	{
		if(!actionStatus.CommonData.TryGetDataValue(ActionParam.SpeedMultiFrom, out var _speedMulti))
			return BaseSpeed;
		return BaseSpeed * _speedMulti;
	}
}