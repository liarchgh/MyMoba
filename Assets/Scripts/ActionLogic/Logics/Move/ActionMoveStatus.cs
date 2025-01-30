using System.Collections.Generic;
using UnityEngine;

public class ActionMoveStatus: ActionStatusBase
{
	public List<ActionLogicGameObjectEntity> _entities = new List<ActionLogicGameObjectEntity>();
	public GameObject Go => _entities[0].GO;
	public bool _moving = false;
	public float dis_last;
}
