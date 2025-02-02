using System;
using System.Collections.Generic;
using UnityEngine;
using GameAction;

[Serializable]
public class ActionLogicContains: ActionLogicWithParamBase<ActionContainsParam, ActionContainsStatus>
{
	public GameObject Prefab;
	public LayerMask TerrainLayer;
	public float ContainTimeLength = 4.0f;
	public override bool FixedUpdateAction(List<object> value, ActionContainsStatus actionStatus)
	{
		var done = TimeUtil.GetTime() > actionStatus.DoTime + ContainTimeLength;
		if (done) Clear(value, actionStatus);
		return done;
	}
	public override void DoActionLogic(List<object> value, ActionCommonData commonData,
		SkillComponent skillComponent, out ActionContainsStatus actionStatus)
	{
		base.DoActionLogic(value, commonData, skillComponent, out actionStatus);
		var go = GameObject.Instantiate(Prefab);
		actionStatus._entities.Add(new ActionLogicGameObjectEntity(){GO = go});
		actionStatus.Go.transform.position = ActionParam.GetPosition(value);
	}
	public override void Clear(List<object> value, ActionContainsStatus actionStatus)
	{
		base.Clear(value, actionStatus);
		actionStatus._entities.ForEach(e => e.Clear());
		actionStatus._entities.Clear();
	}

	protected override ActionContainsStatus CreateStatus()
	{
		return new ActionContainsStatus();
	}
}