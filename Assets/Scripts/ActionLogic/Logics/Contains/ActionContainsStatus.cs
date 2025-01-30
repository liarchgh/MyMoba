using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionContainsStatus: ActionStatusBase
{
	public List<ActionLogicGameObjectEntity> _entities = new List<ActionLogicGameObjectEntity>();
	public GameObject Go => _entities[0].GO;
}
