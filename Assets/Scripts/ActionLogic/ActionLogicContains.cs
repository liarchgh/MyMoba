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
[Serializable]
public class ActionLogicContains: ActionLogicWithParamBase<ActionContainsParam>
{
	public GameObject Prefab;
	public LayerMask TerrainLayer;
	public float ContainTimeLength = 4.0f;
	private List<ActionLogicGameObjectEntity> _entities = new List<ActionLogicGameObjectEntity>();
	private GameObject Go => _entities[0].GO;
	public override bool FixedUpdate(List<object> value)
	{
		var done = TimeUtil.GetTime() > ActionParam.DoTime + ContainTimeLength;
		if (done) Stop(value);
		return done;
	}
	public override void DoLogic(List<object> value)
	{
		base.DoLogic(value);
		var go = GameObject.Instantiate(Prefab);
		_entities.Add(new ActionLogicGameObjectEntity(){GO = go});
		Go.transform.position = ActionParam.GetPosition(value);
	}
	public override void Stop(List<object> value)
	{
		_entities.ForEach(e => e.Clear());
		_entities.Clear();
	}
}