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
public class ActionContainsParam : ActionParam
{
	public override bool TryGenParam()
	{
		return true;
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
	public override void DoLogic()
	{
		Ray target_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit target_hit;
		var go = GameObject.Instantiate(Prefab);
		_entities.Add(new ActionLogicShootEntity(){GO = go});
		if (Physics.Raycast(target_ray, out target_hit, 600f, TerrainLayer.value)) {
			Vector3 pos = target_hit.point;
			Go.transform.position = pos;
			skill2_time = Time.time;
		}
	}
	public override void Clear()
	{
		_entities.ForEach(e => e.Clear());
		_entities.Clear();
	}
}