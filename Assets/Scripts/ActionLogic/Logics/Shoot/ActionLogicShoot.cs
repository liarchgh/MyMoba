using System;
using System.Collections.Generic;
using GameAction;
using UnityEngine;

[Serializable]
public class ActionLogicShoot: ActionLogicWithParamBase<ActionShootParam, ActionShootStatus>
{
	public GameObject Prefab;
	public float Speed = 50;
	public override bool FixedUpdateAction(List<object> value, ActionShootStatus actionStatus)
	{
		return true;
	}
	public override void DoActionLogic(List<object> value, ActionCommonData commonData,
		SkillComponent skillComponent, out ActionShootStatus actionStatus)
	{
		base.DoActionLogic(value, commonData, skillComponent, out actionStatus);
		var go = GameObject.Instantiate(Prefab);
		go.transform.position = ActionParam.GetStartPosition(value);
		var targetPos = ActionParam.GetEndPosition(value);
		bool isEnd(bool start, ref float dis)
		{
			targetPos.y = go.transform.position.y;
			var lastDis = dis;
			dis = (targetPos - go.transform.position).magnitude;
			return !start && lastDis < dis;
		}
		ActorBullet<float>.Create(go, () => {
			targetPos.y = go.transform.position.y;
			return (targetPos - go.transform.position).normalized * Speed;
		}, isEnd);
	}
	protected override ActionShootStatus CreateStatus()
	{
		return new ActionShootStatus();
	}
}