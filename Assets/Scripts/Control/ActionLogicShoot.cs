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
public class ActionLogicShoot: ActionLogicBase
{
	public GameObject PrefabPath;
	private List<ActionLogicShootEntity> Entities = new List<ActionLogicShootEntity>();
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
		var go = GameObject.Instantiate(PrefabPath);
		var e = new ActionLogicShootEntity(){GO = go};
		Entities.Add(e);
		set_skill1_state(state_move, PlayerControl.Instance.transform.position);
	}
	public override void Clear()
	{
		Entities.ForEach(e => e.Clear());
		Entities.Clear();
	}
	private const int state_static = 0;
	private const int state_move = 1;
	private float skill1_dis_last = -55;
	private Vector3 skill1_target_position;
	public LayerMask layer_Terrain;
	public GameObject _skill1Go => Entities[0].GO;
	public Rigidbody skill1_rb => _skill1Go.GetComponent<Rigidbody>();
	public float skill1_speed = 50;
	public void set_skill1_state(int state, Vector3 startPos) {
			// _skill1Go = Instantiate(Skill1Prefab);
			skill1_dis_last = -55;
			Ray target_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit target_hit;
			Vector3 pos;
			if (Physics.Raycast(target_ray, out target_hit, 600f, layer_Terrain.value)) {
				pos = target_hit.point;
				skill1_target_position = pos;
				_skill1Go.transform.position = startPos;
			}
			skill1_target_position.y = _skill1Go.transform.position.y;
			skill1_rb.linearVelocity = (skill1_target_position - _skill1Go.transform.position).normalized * skill1_speed;
	}
}