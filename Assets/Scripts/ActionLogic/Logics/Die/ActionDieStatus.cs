using System.Collections.Generic;
using UnityEngine;

public class ActionDieStatus: ActionStatusBase
{
	public List<ActionLogicGameObjectEntity> _entities =
		new List<ActionLogicGameObjectEntity>();
	public float Speed = 1;
	public GameObject ClickRigPrefab;
	public bool _moving = false;
	public float dis_last;
}
