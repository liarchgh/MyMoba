using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionShootStatus: ActionStatusBase
{
	public List<ActionLogicGameObjectEntity> _entities = new List<ActionLogicGameObjectEntity>();
	public GameObject _go => _entities[0].GO;
	public Rigidbody _rb => _go.GetComponent<Rigidbody>();
	public float _lastDis;
}
