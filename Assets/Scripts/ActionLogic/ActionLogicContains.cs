using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ActionContainsParam : ActionParamBase
{
	[SerializeReference]
	public ActionParamSingleParamBase<Vector3> PositionParam;
	public override bool TryGenParam(out List<object> value)
	{
		var succ = PositionParam.TryGenValue(out var pos);
		value = new List<object>{pos};
		return succ;
	}
	public Vector3 GetPosition(List<object> value)
	{
		return (Vector3)value.First();
	}
}
public class ActionContainsStatus: ActionStatusBase
{
	public float ContainTimeLength = 4.0f;
	public List<ActionLogicGameObjectEntity> _entities = new List<ActionLogicGameObjectEntity>();
	public GameObject Go => _entities[0].GO;
}
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