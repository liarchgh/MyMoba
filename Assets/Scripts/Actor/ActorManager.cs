using System;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour, ICommonManger
{
	public static ActorManager Instance { get; private set; }
	private List<uint> _actors = new List<uint>();
	private Dictionary<uint, ActorBase> _actorDic = new Dictionary<uint, ActorBase>();
	private void Awake()
	{
		Instance = this;
		BattleManger.Instance.AddCommonManger(this);
	}
	public void CommonStart()
	{
	}
	public void CommonUpdate()
	{
		_actors.ForEach(id => _actorDic[id].CommonUpdate());
	}
	public void CommonFixedUpdate()
	{
		_actors.ForEach(id => _actorDic[id].CommonFixedUpdate());
	}
	public void AddActor(ActorBase actor)
	{
		actor.ID = GenNewActorID();
		_actors.Add(actor.ID);
		_actorDic.Add(actor.ID, actor);
		actor.CommonStart();
		LogUtil.Debug($"actor create, actor id: {actor.ID}");
	}
	public void RemoveActor(uint actorID)
	{
		if(!_actorDic.ContainsKey(actorID))
		{
			LogUtil.Error($"actor not exist, actor id: {actorID}");
			return;
		}
		_actors.Remove(actorID);
		_actorDic.Remove(actorID);
	}
	public ActorBase GetActor(uint actorID)
	{
		return _actorDic[actorID];
	}
#region ID
	private uint _nextActorID = 0;
	private uint GenNewActorID()
	{
		return _nextActorID++;
	}
#endregion
}
