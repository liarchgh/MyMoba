using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionLogicContainsEntity : EntityBase
{
	public GameObject GO;
	public override void Clear()
	{
		GameObject.Destroy(GO);
	}
}
[Serializable]
public class ActionContainsParam : ActionParamBase
{
	[SerializeReference, Subclass]
	public ActionParamSinglePositionParamBase Position;
	public override bool TryGenParam(out object value)
	{
		var succ = Position.TryGenValue(out var pos);
		value = pos;
		return succ;
	}
	public Vector3 GetPosition(object value)
	{
		return (Vector3)value;
	}
}
[Serializable]
public class ActionLogicContains: ActionLogicWithParamBase<ActionContainsParam>
{
	public GameObject Prefab;
	public LayerMask TerrainLayer;
	private List<ActionLogicShootEntity> _entities = new List<ActionLogicShootEntity>();
	private GameObject Go => _entities[0].GO;
	private float skill2_time = -55;
	public float skill2_last_time = 4.0f;
	public Vector3 default_position = new Vector3(0, 2000f, 0);
	public override bool FixedUpdate()
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
	public override void DoLogic(object value)
	{
		var go = GameObject.Instantiate(Prefab);
		_entities.Add(new ActionLogicShootEntity(){GO = go});
		Go.transform.position = ActionParam.GetPosition(value);
		skill2_time = Time.time;
	}
	public override void Clear()
	{
		_entities.ForEach(e => e.Clear());
		_entities.Clear();
	}
}