using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ActionContainsParam : ActionParamBase
{
	[SerializeReference, Subclass]
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
	private List<ActionLogicGameObjectEntity> _entities = new List<ActionLogicGameObjectEntity>();
	private GameObject Go => _entities[0].GO;
	private float skill2_time = -55;
	public float skill2_last_time = 4.0f;
	public Vector3 default_position = new Vector3(0, 2000f, 0);
	public override bool FixedUpdate(List<object> value)
	{
		if (skill2_time >= 0) {
			if (Time.time > skill2_time + skill2_last_time) {
				skill2_time = -55;
				Clear();
				return true;
			}
		}
		return false;
	}
	public override void DoLogic(List<object> value)
	{
		var go = GameObject.Instantiate(Prefab);
		_entities.Add(new ActionLogicGameObjectEntity(){GO = go});
		Go.transform.position = ActionParam.GetPosition(value);
		skill2_time = Time.time;
	}
	public override void Clear()
	{
		_entities.ForEach(e => e.Clear());
		_entities.Clear();
	}
}