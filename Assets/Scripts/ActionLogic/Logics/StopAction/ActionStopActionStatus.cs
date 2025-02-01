using System.Collections.Generic;
using UnityEngine;

public class ActionStopActionStatus: ActionStatusBase
{
	public List<ActionLogicGameObjectEntity> _entities = new List<ActionLogicGameObjectEntity>();
	public bool _moving = false;
	public float dis_last;
}
