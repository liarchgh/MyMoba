using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ActionLogicContains: ActionLogicWithParamBase<ActionContainsParam, ActionContainsStatus>
{
	public GameObject Prefab;
	public LayerMask TerrainLayer;
	public float ContainTimeLength = 4.0f;
	public override bool FixedUpdateAction(List<object> value, ActionContainsStatus containsStatus)
	{
		var done = TimeUtil.GetTime() > containsStatus.DoTime + ContainTimeLength;
		if (done) StopAction(value, containsStatus);
		return done;
	}
	public override void DoActionLogic(List<object> value, out ActionContainsStatus containsStatus)
	{
		base.DoActionLogic(value, out containsStatus);
		var go = GameObject.Instantiate(Prefab);
		containsStatus._entities.Add(new ActionLogicGameObjectEntity(){GO = go});
		containsStatus.Go.transform.position = ActionParam.GetPosition(value);
	}
	public override void StopAction(List<object> value, ActionContainsStatus containsStatus)
	{
		containsStatus._entities.ForEach(e => e.Clear());
		containsStatus._entities.Clear();
	}

    protected override ActionContainsStatus CreateStatus()
    {
        return new ActionContainsStatus();
    }
}