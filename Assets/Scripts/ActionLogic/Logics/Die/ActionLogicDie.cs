using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionLogicDie: ActionLogicWithParamBase<ActionDieParam, ActionDieStatus>
{
	public float Speed = 1;
	public GameObject ClickRigPrefab;
	public override bool FixedUpdateAction(List<object> value, ActionDieStatus dieStatus)
	{
		if(!dieStatus._moving) return true;

		var rb = ActionParam.GetActionRigidbody(value);
		var pos = ActionParam.GetTargetPosition(value);
		//用和目的地距离变远作为终止条件 但是在碰撞后被弹回会停止
		pos.y = rb.position.y;
		float dis_now = Mathf.Abs(Vector3.Distance(pos, rb.position));
		if ((dis_now <= dieStatus.dis_last || dieStatus.dis_last < 0) && dis_now > 0.2f) {
			dieStatus.dis_last = dis_now;
		} else {
			rb.linearVelocity = Vector3.zero;
			StopAction(value, dieStatus);
			dieStatus._moving = false;
		}
		return !dieStatus._moving;
	}
	public override void DoActionLogic(List<object> value, out ActionDieStatus dieStatus)
	{
		base.DoActionLogic(value, out dieStatus);
		var rb = ActionParam.GetActionRigidbody(value);
		var pos = ActionParam.GetTargetPosition(value);
		var rig = GameObject.Instantiate(ClickRigPrefab);
		rig.transform.position = pos;
		dieStatus._entities.Add(new ActionLogicGameObjectEntity(){GO = rig});

		pos.y = rb.position.y;
		rb.linearVelocity = (pos - rb.position).normalized * Speed;
		dieStatus.dis_last = -1;
		dieStatus._moving = true;
	}

	public override void StopAction(List<object> value, ActionDieStatus dieStatus)
	{
		dieStatus._entities.ForEach(e => e.Clear());
		dieStatus._entities.Clear();
	}

    protected override ActionDieStatus CreateStatus()
    {
        return new ActionDieStatus();
    }
}