using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionLogicShootEntity : EntityBase
{
	public GameObject GO;
	public override void Clear()
	{
		GameObject.Destroy(GO);
	}
}
[Serializable]
public class ActionShootParam: ActionParam
{
	[SerializeReference, Subclass]
	public ActionParamSinglePositionParamBase StartPositionGen;
	[SerializeReference, Subclass]
	public ActionParamSinglePositionParamBase EndPositionGen;

	public override bool TryGenParam()
	{
		return StartPositionGen.TryGenValue() && EndPositionGen.TryGenValue();
	}
}
[Serializable]
public class ActionLogicShoot: ActionLogicWithParamBase<ActionShootParam>
{
	public GameObject Prefab;
	private List<ActionLogicShootEntity> _entities = new List<ActionLogicShootEntity>();
	public override bool FixedUpdate()
	{
		float dis_now = Mathf.Abs(Vector3.Distance(skill1_target_position, _skill1Go.transform.position));
		if (dis_now <= skill1_dis_last || skill1_dis_last < 0) {
			skill1_target_position.y = _skill1Go.transform.position.y;
			// skill1_rb.velocity = (skill1_target_position - skill1.transform.position).normalized * skill1_speed;
			skill1_dis_last = dis_now;
			return false;
		} else {
			Clear();
			return true;
		}
	}
	public override void DoLogic()
	{
		var go = GameObject.Instantiate(Prefab);
		var e = new ActionLogicShootEntity(){GO = go};
		_entities.Add(e);
		skill1_dis_last = -55;
		skill1_target_position = ActionParam.EndPositionGen.Value;
		_skill1Go.transform.position = ActionParam.StartPositionGen.Value;
		skill1_target_position.y = _skill1Go.transform.position.y;
		skill1_rb.linearVelocity = (skill1_target_position - _skill1Go.transform.position).normalized * skill1_speed;
	}
	public override void Clear()
	{
		_entities.ForEach(e => e.Clear());
		_entities.Clear();
	}
	private float skill1_dis_last = -55;
	private Vector3 skill1_target_position;
	public GameObject _skill1Go => _entities[0].GO;
	public Rigidbody skill1_rb => _skill1Go.GetComponent<Rigidbody>();
	public float skill1_speed = 50;
}