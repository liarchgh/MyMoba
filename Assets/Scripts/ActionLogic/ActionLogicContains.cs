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
	public override bool TryGenParam()
	{
		return Position.TryGenValue();
	}
	public override ActionParamBase GenCopy()
	{
		return new ActionContainsParam() {
			Position = Position,
		};
	}
}
[Serializable]
public class ActionLogicContains: ActionLocigWithParamBase<ActionContainsParam>
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
	public override void DoLogic()
	{
		var go = GameObject.Instantiate(Prefab);
		_entities.Add(new ActionLogicShootEntity(){GO = go});
		Go.transform.position = ActionParam.Position.Value;
		skill2_time = Time.time;
	}
	public override void Clear()
	{
		_entities.ForEach(e => e.Clear());
		_entities.Clear();
	}
}